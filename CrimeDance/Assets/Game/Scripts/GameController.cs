using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SongData;
using System.IO;

public class GameController : MonoBehaviour {


    public static bool gameStart = false;
    public bool createMode = false;
    int points;
    public Text pointsText;

    public GameObject hitBar;

    public GameObject timerTest;
    public bool TimerTestCompleted = false;


    public static GameController instance;

    public List<Note> activeNotes;
    public List<GameObject> triggerAreas;


     int PerfectDifficulty = 8;
     int GoodDifficulty = 10;
     int OkayDifficulty = 12;
     int BadDifficulty = 14;


    //gold note logic
    public bool goldRush = false;
    public bool goldRushFailed = false;

    //row logic
    public Note activeNote;


    //player logic
    public GameObject PlayerCharacter;
    int shotsFired = 0;
    public int gunShotsUsed = 0;
    bool targetEliminated = false;

    int alright = 0;
    int swell = 0;
    int swanky = 0;
    int miss = 0;

    public GameObject endScreen;


    //create mode logic
    public NoteType noteTypeMode = NoteType.normal;
    public Image createModeSign;
    public GameObject createModeMenu;


    void Awake()
    {
        instance = this;
        createMode = MenuScript.InCreateMode;
        activeNotes = new List<Note>();

        if (createMode)
        {
            createModeMenu.SetActive(true);
            createModeSign.gameObject.SetActive(true);
        }
        PlaceTarget();
    }

	void FixedUpdate () {

        SongRecorder.Update();

        if (!TimerTestCompleted)
        {
            timerTest.transform.localPosition += new Vector3(0, MusicSheetMover.instance.speed, 0);
        }

        if (activeNote == null && (activeNotes.Count > 0))
        {
            activeNote = activeNotes[0];
        }

        if (shotsFired >= 3)
        {
            Debug.Log("Target dead");
            MusicSheetMover.instance.audio.volume = 1.0f;
            if(targetEliminated != true)
            {
                foreach(LocationScript loc in locs)
                {
                    if (loc.HasTarget)
                    {
                        loc.gameObject.transform.FindChild("Target").GetComponent<Animator>().Play("TargetDie");
                    }
                }
                GameObject eliminated = (GameObject)Instantiate(PrefabHolder.instance.Eliminated);
                eliminated.transform.SetParent(GameObject.Find("Canvas").transform);
                eliminated.transform.localPosition = new Vector3(100, 160, 0);
                MusicSheetMover.instance.AddDoorNote();
            }
            targetEliminated = true;
            ExitTime = true;
        }
        if (gunShotsUsed >= 3)
        {
            MusicSheetMover.instance.audio.volume = 1.0f;
            MusicSheetMover.instance.TimeToShoot = false;
            if(!targetEliminated)
            {
                if (PlayerCharacter.GetComponent<Player>().currentLocation.HasTarget)
                {
                    MusicSheetMover.instance.TimeToShoot = true;
                    MusicSheetMover.instance.shotsCreated = 0;
                    shotsFired = 0;
                    gunShotsUsed = 0;
                }
            }     
        }

        if (gameoverstate)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Back");
                Application.LoadLevel(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1)) {
            var song = SongRecorder.EndRecording();
            var songData = song.JsonSave();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path + "/DanceDanceAssassination/Steps/"))
            { 
                System.IO.Directory.CreateDirectory(path + "/DanceDanceAssassination/Steps/");
            }
            File.WriteAllText(path+ "/DanceDanceAssassination/Steps/" + song.DisplayName + ".json", songData.PrettyPrint());
            Debug.Log("Saved file " + song.DisplayName + ".json");
            Application.LoadLevel(0);
        }
    }


    public void TriggerEventAdd(TriggerAreaScript triggerArea)
    {
        activeNotes.Add(triggerArea.currentNote);
        
    }

    public void TriggerEventRemove(Note note)
    {
        Note temp = note;
        if (goldRush)
        {
            if (temp.NoteType == NoteType.golden)
            {
                Debug.Log("Gold rush over");
                GameObject.Find("StealProg").GetComponent<Animator>().Play("Disappear");
                miss++;
                Lifebar.instance.DecreaseLife(-10f);
                goldRushFailed = true;
                goldRush = false;
            }
        }
        else if(temp.NoteType == NoteType.actionGUN)
        {
            gunShotsUsed++;
        }
        else if (temp.NoteType == NoteType.actionESCAPE)
        {
            MusicSheetMover.instance.AddDoorNote();
        }
        else if (temp.isStarter || temp.NoteType == NoteType.action)
        {

        }
        else
        {
            Lifebar.instance.DecreaseLife(-10f);
            miss++;
        }
        // triggerAreas[temp.Row].GetComponent<Animator>().Play("MissHitCircle");
        activeNotes.Remove(temp);
        MusicSheetMover.instance.playList.Remove(temp.gameObject);
        Debug.Log("Missed key");
        Destroy(temp.GetComponent<BoxCollider2D>());
        Destroy(temp.gameObject);
    }

    public void RegisterButtonAutoWin()
    {
        if (activeNotes.Count > 0)
        {
            Debug.Log("Note cleared");
            Note temp = activeNotes[0];
            triggerAreas[temp.Row].GetComponent<Animator>().Play("SucessHit");
           // int testTime = temp.PlayTime;
           // testTime += MusicSheetMover.TimingAdjustment * 2;
           // Debug.Log("Clearing time: " + MusicSheetMover.instance.currentTime + " - Note time: " + testTime);
            TestHitQualityZone();
            activeNotes.Remove(temp);
            MusicSheetMover.instance.playList.Remove(temp.gameObject);
            Destroy(temp.GetComponent<BoxCollider2D>());
            temp.GetComponent<Animator>().Play("Disappear");
        }
    }


    public void RegisterButtonPress(Direction button)
    {


           if(!(activeNote == null))
            {

                if(activeNote.NoteType == NoteType.action)
                {
                    ChangePlayerLocation(button);
                    AudioSource audio = triggerAreas[activeNote.Row].GetComponent<AudioSource>();
                    audio.PlayOneShot(PrefabHolder.instance.snap);
                    activeNotes.Remove(activeNote);
                    MusicSheetMover.instance.playList.Remove(activeNote.gameObject);
                    Destroy(activeNote.GetComponent<BoxCollider2D>());
                    activeNote.GetComponent<Animator>().Play("Disappear");
                    activeNote = null;
                    return;
                }
                if(activeNote.NoteType == NoteType.actionGUN)
                {
                    MusicSheetMover.instance.audio.volume = 0.3f; //audio only lowered, not silenced completely
                    shotsFired++;
                    gunShotsUsed++;
                    
                    AudioSource audio = triggerAreas[activeNote.Row].GetComponent<AudioSource>();
                    audio.PlayOneShot(PrefabHolder.instance.shoot);
                    activeNotes.Remove(activeNote);
                    MusicSheetMover.instance.playList.Remove(activeNote.gameObject);
                    Destroy(activeNote.GetComponent<BoxCollider2D>());
                    activeNote.GetComponent<Animator>().Play("Disappear");
                    activeNote = null;
                    return;
                }
            if (activeNote.NoteType == NoteType.actionESCAPE)
            {
                if (AtDoor)
                {
                    //MusicSheetMover.instance.audio.Stop();
                    GameObject.Find("Door").GetComponent<Animator>().Play("DoorOpen");
                    AudioSource audio = triggerAreas[activeNote.Row].GetComponent<AudioSource>();
                    audio.PlayOneShot(PrefabHolder.instance.drum);
                    Escaped = true;
                    GameOver();
                }
                else
                {
                    MusicSheetMover.instance.AddDoorNote();
                    AudioSource audio = triggerAreas[activeNote.Row].GetComponent<AudioSource>();
                    audio.PlayOneShot(PrefabHolder.instance.snap);
                }
                activeNotes.Remove(activeNote);
                MusicSheetMover.instance.playList.Remove(activeNote.gameObject);
                Destroy(activeNote.GetComponent<BoxCollider2D>());
                activeNote.GetComponent<Animator>().Play("Disappear");
                activeNote = null;
                return;
            }


            if (activeNote.direction == button)
                {

                    triggerAreas[activeNote.Row].GetComponent<Animator>().Play("SucessHit");
                    AudioSource audio = triggerAreas[activeNote.Row].GetComponent<AudioSource>();
                    if (activeNote.NoteType == NoteType.golden)
                    {
                        audio.PlayOneShot(PrefabHolder.instance.drum);
                        if (activeNote.isStarter)
                        {
                            goldRush = true;
                            GameObject stealProgess = (GameObject)Instantiate(PrefabHolder.instance.Stealing.gameObject);
                            stealProgess.name = "StealProg";
                            stealProgess.transform.SetParent(GameObject.Find("Canvas").transform);
                            stealProgess.transform.localPosition = new Vector3(370, 50, 0);
                            goldRushFailed = false;
                        }
                        if (activeNote.isEndNote)
                        {
                            goldRush = false;
                            GameObject.Find("StealProg").GetComponent<Animator>().Play("Disappear");
                            Debug.Log("Gold rush succeeded");
                            points *= 2;
                            pointsText.text = points.ToString();
                        }
                    }
                    else
                    {
                        audio.PlayOneShot(PrefabHolder.instance.snap);
                    }

                    double testTime = activeNote.PlayTime;
                    testTime += MusicSheetMover.TimingAdjustment * 2;
                    //Debug.Log("Clearing time: " + MusicSheetMover.instance.currentTime + " - Note time: " + testTime);
                    // TestHitQuality(testTime, MusicSheetMover.instance.currentTime);
                    TestHitQualityZone(); 
                }
                else if (!(activeNote.direction == button)) //Is it interesting without wrong keys?
                {
                /*Debug.Log("Wrong key");
                miss++;
                if (goldRush)
                {
                    if (activeNoteType == NoteType.golden)
                    {
                        Debug.Log("Gold rush over");
                        GameObject.Find("StealProg").GetComponent<Animator>().Play("Disappear");
                        goldRushFailed = true;
                        goldRush = false;
                    }
                }

                triggerAreas[activeNote.Row].GetComponent<Animator>().Play("MissHitCircle");
                Lifebar.instance.DecreaseLife(-10f);*/
                return;
                }

            activeNotes.Remove(activeNote);
            MusicSheetMover.instance.playList.Remove(activeNote.gameObject);
            Destroy(activeNote.GetComponent<BoxCollider2D>());
            activeNote.GetComponent<Animator>().Play("Disappear");
            activeNote = null;
        }
      
    }

    public void TestHitQualityZone()
    {
        GameObject textFeedback = null;
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        switch (activeNotes[0].hitType)
        {
            case Note.HitType.poor:
                textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Bad.gameObject); //Fix this! Bad to normal
                textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
                alright++;
                textFeedback.transform.localPosition = new Vector3(x, y, 0);
                points += 3;
                pointsText.text = points.ToString();
                break;
            case Note.HitType.good:
                swell++;
                textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Good.gameObject);
                textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
                textFeedback.transform.localPosition = new Vector3(x, y, 0);
                points += 10;
                pointsText.text = points.ToString();
                break;
            case Note.HitType.perfect:
                swanky++;
                textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Swanky.gameObject);
                textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
                textFeedback.transform.localPosition = new Vector3(x, y, 0);
                points += 25;
                Lifebar.instance.DecreaseLife(1f);
                pointsText.text = points.ToString();
                break;
            default: break;
        }
    }


    public void TestHitQuality(int noteTime, int hitTime)
    {
        int difference = Mathf.Abs(noteTime-hitTime);
        Debug.Log(difference);

        if (difference < PerfectDifficulty)
        {
            GameObject textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Swanky.gameObject);
            textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
            textFeedback.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if(difference < GoodDifficulty)
        {
            GameObject textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Good.gameObject);
            textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
            textFeedback.transform.localPosition = new Vector3(-10, 0, 0);
        }
        else if(difference < OkayDifficulty)
        {
            GameObject textFeedback = (GameObject)Instantiate(PrefabHolder.instance.Bad.gameObject); //Fix this! Bad to normal
            textFeedback.transform.SetParent(GameObject.Find("Canvas").transform);
            textFeedback.transform.localPosition = new Vector3(-15, 0, 0);
        }
        else if (difference < BadDifficulty)
        {
            
        }

    }

    new public Camera camera;
    public GameObject LocationHolder;
    public bool ExitTime = false;
    public bool AtDoor = false;
    public bool AtTarget = false;
    public bool LeftAwayTarget = false; 
    public bool Escaped = false;

    List<LocationScript> locs;
    public void PlaceTarget(){
        locs = new List<LocationScript>();
        int childCount = LocationHolder.transform.childCount-1;
        for(int i = 0; i < childCount; i++)
        {
            locs.Add(LocationHolder.transform.GetChild(i).GetComponent<LocationScript>());
        }

        int rando = Random.Range(1, locs.Count - 1);
        locs[rando].HasTarget = true;
        LocationHolder.transform.GetChild(rando).FindChild("Target").gameObject.SetActive(true);
        LocationHolder.transform.GetChild(rando).FindChild("Customer").gameObject.SetActive(false);

    }


    public void ChangePlayerLocation(Direction dir)
    {
        
        LocationScript loc = PlayerCharacter.GetComponent<Player>().currentLocation;
        int direction = 0;
        switch (dir)
        {
            case Direction.Up:
                direction = 2;
                break;
            case Direction.UR:
                break;
            case Direction.Right:
                direction = 0;
                break;
            case Direction.DR:
                break;
            case Direction.Down:
                direction = 3;
                break;
            case Direction.DL:
                break;
            case Direction.Left:
                direction = 1;
                break;
            case Direction.UL:
                break;
            default:
                break;
        }

        if(loc.GetNeighbour(direction) == null)
        {
            return;
        }
        if (AtDoor) //if able to move and at door - no longer at door
        {
            AtDoor = false;
        }
        if (AtTarget)
        {
            AtTarget = false;
            LeftAwayTarget = true;
        }
        PlayerCharacter.transform.SetParent(loc.GetNeighbour(direction).gameObject.transform);
        PlayerCharacter.transform.localPosition = new Vector3(0, 2, 0);
        camera.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -6.5f);
        PlayerCharacter.GetComponent<Player>().currentLocation = loc.GetNeighbour(direction);
        loc.HasPlayer = false;
        loc.GetNeighbour(direction).HasPlayer = true;
        camera.gameObject.transform.rotation = Quaternion.Euler(9, loc.GetNeighbour(direction).CameraAngle, 360);

        if (PlayerCharacter.GetComponent<Player>().currentLocation.HasTarget)
        {
            Debug.Log("Target Acquired");
            PlayerCharacter.GetComponent<AudioSource>().PlayOneShot(PrefabHolder.instance.loadGun);
            MusicSheetMover.instance.TimeToShoot = true;
            MusicSheetMover.instance.shotsCreated = 0;
            shotsFired = 0;
            gunShotsUsed = 0;
            AtTarget = true;
            LeftAwayTarget = false;
        }
        if (ExitTime)
        {
            if (PlayerCharacter.GetComponent<Player>().currentLocation.LocationName.Equals("Exit"))
            {
                AtDoor = true;
            }
        }

    }

    public void CreateNote(Direction button)
    {
        SongRecorder.AddNote(button);

        Note newNote = (Note)Instantiate(MusicSheetMover.instance.noteBase);
        newNote.direction = button;
        newNote.Row = Random.Range(0, 4);
        newNote.PlayTime = MusicSheetMover.instance.Time;
        newNote.NoteType = noteTypeMode;
        GameObject songContainer = GameObject.Find("Song");
        newNote.gameObject.transform.SetParent(songContainer.transform);
        //MusicSheetMover.instance.musicList.Add(newNote.gameObject);
        Debug.Log("Added note to the " + newNote.direction.ToString() + " at time " + newNote.PlayTime);
    }

    public bool gameoverstate = false;
    public void GameOver()
    {
        gameStart = false;
        gameoverstate = true;
        MusicSheetMover.instance.playList.Clear();
        MusicSheetMover.instance.audio.Stop();
        endScreen.SetActive(true);
        endScreen.transform.SetAsLastSibling();
        endScreen.GetComponent<Animator>().Play("EndScreenAppear");
        endScreen.transform.FindChild("Swanky").GetChild(0).GetComponent<Text>().text = swanky.ToString();
        endScreen.transform.FindChild("Swell").GetChild(0).GetComponent<Text>().text = swell.ToString();
        endScreen.transform.FindChild("Alright").GetChild(0).GetComponent<Text>().text = alright.ToString();
        endScreen.transform.FindChild("Miss").GetChild(0).GetComponent<Text>().text = miss.ToString();
        endScreen.transform.FindChild("EndPoints").GetChild(0).GetComponent<Text>().text = points.ToString();
        endScreen.transform.FindChild("SongName").GetComponent<Text>().text = MusicSheetMover.instance.SongName;
        if (targetEliminated)
        {
            endScreen.transform.FindChild("TargetEliminated").GetChild(0).GetComponent<Text>().text = "Yes";

        }
        if (Escaped)
        {
            endScreen.transform.FindChild("Escaped").GetChild(0).GetComponent<Text>().text = "Yes";

        }

        var song = SongRecorder.EndRecording();

    }


}
