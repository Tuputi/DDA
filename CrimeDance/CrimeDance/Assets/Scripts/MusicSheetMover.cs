using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    double previousTime = 0;
    public double currentTime;

    [Range(-10f, -1f)]
    public float speed;



    //shooting logic
    public bool TimeToShoot;
    public int shotsCreated = 0;


    public static double TimingAdjustment;


    void Start()
    {
        instance = this;
        //MenuScript menu = GameObject.Find("MenuScript").GetComponent<MenuScript>();
        audio.clip = MenuScript.song;
        Debug.Log(audio.clip.name);
        trackLenght = System.Math.Round(audio.clip.length, 2);
        musicName = MenuScript.NoteListName;
        SongName = MenuScript.SongName;
        audio.Play();
        playList = new List<GameObject>();
        if (!GameController.instance.createMode)
        {
            LoadMusic(musicName);
        }
    }


    void LoadMusic(string songName)
    {
        SaveLoad.SongXml song = SaveLoad.SongSaveLoad.Load(songName);
        List<SaveLoad.NoteXml> songNotes = song.SongNotes;
        List<GameObject> noteList = new List<GameObject>();
        foreach(SaveLoad.NoteXml nXml in songNotes)
        {
            Note newNote = (Note)Instantiate(noteBase);
            newNote.PlayTime = nXml.time;
            newNote.direction = (ControllerTest.ButtonType)nXml.dir;
            newNote.Row = (int)newNote.direction;
            newNote.noteType = (Note.NoteType)nXml.type;
           // newNote.isGolden = nXml.isGolden;
            newNote.isStarter = nXml.isStarter;
            newNote.isEndNote = nXml.isEndNote;
            noteList.Add(newNote.gameObject);
            newNote.transform.SetParent(GameObject.Find("Song").transform);
         
        }
        musicList = noteList;
    }


    double twodecimalTime;
    public double Time;

    void FixedUpdate()
    {
        twodecimalTime = System.Math.Round(audio.time, 2);
        currentTime = twodecimalTime;
        if (Time != currentTime)
        {
            float test = (float)(Time - currentTime);
            if (Mathf.Abs(test) >= 0.5)
            {
                

                //previousTime = currentTime;
                Time += 0.5;
               // Debug.Log(Time + "/" + trackLenght);
                //Debug.Log(audio.time + "/" + trackLenght);
                if (GameController.gameStart && !GameController.instance.createMode)
                {
                    AddNote();
                }
            }
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
                if (MusicSheet.transform.GetChild(j).GetComponent<Note>().noteType == Note.NoteType.golden)
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
                if (MusicSheet.transform.GetChild(j).GetComponent<Note>().noteType == Note.NoteType.actionGUN)
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


    void AddNote()
    {
        List<GameObject> remove = new List<GameObject>();
        foreach(GameObject n in musicList)
        {
            double testTime = n.GetComponent<Note>().PlayTime;
            if (testTime-TimingAdjustment == Time)
            {
                bool create = false;
                if (n.GetComponent<Note>().noteType == Note.NoteType.golden)
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
                                n.GetComponent<Note>().noteType = Note.NoteType.actionGUN;
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
                        if (!(n.GetComponent<Note>().noteType == Note.NoteType.golden))
                        {
                            n.GetComponent<Note>().noteType = Note.NoteType.actionESCAPE;
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
