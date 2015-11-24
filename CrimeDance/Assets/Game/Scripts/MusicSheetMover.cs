using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SongData;
using System.Linq;

public class MusicSheetMover : MonoBehaviour {

    public GameObject MusicSheet;
    public List<GameObject> musicList;
    public List<GameObject> playList;
    public Note noteBase;
    public static MusicSheetMover instance;

    public bool autoplay = false;
    public string musicName;
    public string SongName;

    public AudioSource audio;

    double trackLenght;

    [Range(-10f, -1f)]
    public float speed;



    //shooting logic
    public bool TimeToShoot;
    public int shotsCreated = 0;

    public double TimeScale;

    public static double TimingAdjustment;


    void Start()
    {
        

        instance = this;
        //MenuScript menu = GameObject.Find("MenuScript").GetComponent<MenuScript>();
        audio.clip = MenuScript.songAudioClip;
        Debug.Log(audio.clip.name);
        trackLenght = System.Math.Round(audio.clip.length, 2);
        SongName = MenuScript.SongName;
        audio.Play();
        playList = new List<GameObject>();
        if (!GameController.instance.createMode)
        {
            //LoadMusic(musicName);
            LoadMusic(SongData.SongImportExport.LoadSong(MenuScript.StepListName));
            Debug.Log(MenuScript.StepListName);
        }
        else
        {
            SongRecorder.StartRecording(MenuScript.StepListName, MenuScript.SongName, MenuScript.ArtistName, MenuScript.MusicName, MenuScript.BMP, SongRecordingSnapping.Quater, SongDifficulty.Easy);
        }
    }


   /* void LoadMusic(string songName)
    {
        TimeScale = 1;
        SaveLoad.SongXml song = SaveLoad.SongSaveLoad.Load(songName);
        List<SaveLoad.NoteXml> songNotes = song.SongNotes;
        List<GameObject> noteList = new List<GameObject>();
        foreach(SaveLoad.NoteXml nXml in songNotes)
        {
            Note newNote = (Note)Instantiate(noteBase);
            newNote.PlayTime = nXml.time;
            newNote.direction = (Direction)nXml.dir;
            newNote.Row = nXml.row;
            newNote.NoteType = (NoteType)nXml.type;
           // newNote.isGolden = nXml.isGolden;
            newNote.isStarter = nXml.isStarter;
            newNote.isEndNote = nXml.isEndNote;
            noteList.Add(newNote.gameObject);
            newNote.transform.SetParent(GameObject.Find("Song").transform);
         
        }
        musicList = noteList;
    }*/

    void LoadMusic(SongData.Song song)
    {
        TimeScale = (int)song.BeatsPerSecond / 60.0f;
        Debug.Log("TimeScale = " + TimeScale);

        List<GameObject> noteList = new List<GameObject>();

        var parent = GameObject.Find("Song").transform;
        foreach (var jsonNote in song.SongNotes) {
            Note note = Instantiate(noteBase) as Note;
            note.PlayTime = jsonNote.Beat;
            note.direction = jsonNote.Direction;
            note.NoteType = jsonNote.NoteType;
            note.Row = DirectionRowHelper.GetRowFromDirection(note.direction);
            noteList.Add(note.gameObject);
            note.transform.SetParent(parent);
        }

        musicList = noteList;
    }

    public double Time;

    void FixedUpdate()
    {
        var oldTime = Time;
        Time = audio.time * TimeScale;

        if (GameController.gameStart && !GameController.instance.createMode)
        {
            AddNote(oldTime, Time);
        }

        if (playList.Count > 0)
        {
            foreach (GameObject g in playList)
            {
                g.transform.localPosition += new Vector3(0, speed, 0);
            }
        }

        if (musicList.Count > 0 && autoplay)
        {
            AutoPlay();
        }
        List<GameObject> destroyThese = new List<GameObject>();
        if (GameController.instance.goldRushFailed)
        {
            Debug.Log("Gold rush failed");
            int i = MusicSheet.transform.childCount;
            for(int j = 0; j < i; j++)
            {
                if (MusicSheet.transform.GetChild(j).GetComponent<Note>().NoteType == NoteType.golden)
                {
                    //Debug.Log("IS golden");
                    if (!(MusicSheet.transform.GetChild(j).GetComponent<Note>().isStarter))
                    {
                       // Debug.Log("Is not starter");
                        destroyThese.Add(MusicSheet.transform.GetChild(j).gameObject);

                    }
                }
            }
            foreach (GameObject n in destroyThese)
            {
                GameController.instance.activeNotes.Remove(n.GetComponent<Note>());
                playList.Remove(n.gameObject);
               // Debug.Log("Gold key removed");
                Destroy(n.GetComponent<BoxCollider2D>());
                n.GetComponent<Animator>().Play("GoldNoteDisappear");
            }
        }
        if (GameController.instance.LeftAwayTarget)
        {
            Debug.Log("LeftTarget!");
            int i = MusicSheet.transform.childCount;
            for (int j = 0; j < i; j++)
            {
                if (MusicSheet.transform.GetChild(j).GetComponent<Note>().NoteType == NoteType.actionGUN)
                {
                     destroyThese.Add(MusicSheet.transform.GetChild(j).gameObject);
                }
            }
            foreach (GameObject n in destroyThese)
            {
                GameController.instance.activeNotes.Remove(n.GetComponent<Note>());
                playList.Remove(n.gameObject);
                // Debug.Log("Gold key removed");
                Destroy(n.GetComponent<BoxCollider2D>());
                n.GetComponent<Animator>().Play("GoldNoteDisappear");
            }
            GameController.instance.LeftAwayTarget = false;
            GameController.instance.gunShotsUsed = 3;
        }

        if (Time >= trackLenght)
        {
            GameController.instance.GameOver();
        }
    }



    void AutoPlay()
    {
        foreach(Note n in GameController.instance.activeNotes)
        {
            if(n.hitType == Note.HitType.perfect)
            {
                GameController.instance.RegisterButtonAutoWin();
                break;
            }
        }
    }


    void AddNote(double oldTime, double currentTime)
    {
        var oldTimeAdjusted = oldTime - TimingAdjustment;
        var currentTimeAdjusted = currentTime - TimingAdjustment;
        Debug.Log(string.Format("{0}, {1}", oldTimeAdjusted, currentTimeAdjusted));


        List<GameObject> remove = new List<GameObject>();
        foreach(GameObject n in musicList.ToList())
        {
            double testTime = n.GetComponent<Note>().PlayTime;

            if (testTime.Between(oldTimeAdjusted, currentTimeAdjusted))
            {
                bool create = false;
                if (n.GetComponent<Note>().NoteType == NoteType.golden)
                {
                    if (n.GetComponent<Note>().isStarter)
                    {
                        create = true;
                    }
                    else if (!(n.GetComponent<Note>().isStarter) && GameController.instance.goldRushFailed)
                    {
                        create = false;
                    }
                    else if (!(n.GetComponent<Note>().isStarter) && GameController.instance.goldRush)
                    {
                        create = true;
                    }
                }
                else
                {
                    create = true;
                }

                if (create)
                {

                   
                    if (TimeToShoot)
                    {
                        if(shotsCreated < 3)
                        {
                            if(!(n.GetComponent<Note>().isStarter || n.GetComponent<Note>().isEndNote))
                            {
                                n.GetComponent<Note>().NoteType = NoteType.actionGUN;
                                shotsCreated++;
                            }
                            
                        }
                        /*else
                        {
                            audio.volume = 1.0f;
                            TimeToShoot = false;
                        }*/
                    }
                    if (addDoor && doorAppearCounter <= 0)
                    {
                        if (!(n.GetComponent<Note>().NoteType == NoteType.golden))
                        {
                            n.GetComponent<Note>().NoteType = NoteType.actionESCAPE;
                            addDoor = false;
                        }
                    }

                    GameObject newNote = Instantiate(n);
                    newNote.transform.SetParent(MusicSheet.transform);
                    float row = 0;
                    switch (n.GetComponent<Note>().Row)
                    {
                        case 0:
                            row = 93;
                            break;
                        case 1:
                            row = 176;
                            break;
                        case 2:
                            row = 260;
                            break;
                        case 3:
                            row = 343;
                            break;
                        default: break;
                    }
                    newNote.transform.localPosition = new Vector3(row, -100, 0);
                    playList.Add(newNote);
                    remove.Add(n);
                    Debug.Log("Note added");
                }
            }
        }
        foreach (GameObject go in remove) {
            musicList.Remove(go);
        }
        if(doorAppearCounter > 0)
        {
            doorAppearCounter--;
        }
    }


   public bool addDoor = false;
    public int doorAppearCounter = 0;
    public void AddDoorNote()
    {
        addDoor = true;
        doorAppearCounter = 40;
    }
}
