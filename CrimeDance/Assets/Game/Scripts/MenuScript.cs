using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;


public class MenuScript : MonoBehaviour {


    //info about song and steplist
    public AudioClip songAudioClip;
    public static string DisplayName; //what the user sees as the name
    public static string SongName; //what the song is called 
    public static string ArtistName; // what the artist is called
    public static string MusicName; //what the music-file is called
    public static string StepListName; //what the steplist-file is called
    public static int BMP;


    //references to objects in scene
    public GameObject instructions;
    public static bool InCreateMode = false;
    public static MenuScript instance;

    public InputField Filename;
    public InputField BmpField;
    public Dropdown songDropdown;

    public GameObject ButtonPrefab;
    public GameObject ButtonHolder;
    public GameObject CreateModeObject;

    //go through different songs/levels
    public List<GameObject> SongButtonList;
    public int currentButtonIndex = 0;
    GameObject currentButton;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        GameObject.Find("Instructions").GetComponent<UnityEngine.UI.Button>().Select(); //Doesn't activate always on the dancepad?
        CreateButtons();
        MoveButtons();
    }

    public void CreateButtons()
    {
        SongButtonList = new List<GameObject>();
        Debug.Log("Createbuttons");
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        DirectoryInfo dir = new DirectoryInfo(path + "/DanceDanceAssassination/Steps/");
        FileInfo[] info = dir.GetFiles();
        foreach (FileInfo f in info)
        {
            SongData.Song song = SongData.SongImportExport.LoadSongPath(f.ToString());
            GameObject button = Instantiate(ButtonPrefab);
            button.GetComponent<SongSelectButton>().DisplayName = song.DisplayName;
            button.GetComponent<SongSelectButton>().SongDisplayName = song.SongName;
            button.GetComponent<SongSelectButton>().ArtistDisplayName = song.ArtistName;

            button.name = song.DisplayName;
            button.SetActive(false);
            SongButtonList.Add(button);
        }  
    }

    void Update()
    {
        WatchInput();
    }

    public void WatchInput()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            DecreseIndex();  
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            IncreaseIndex();
        }
    }

    public void DecreseIndex()
    {
        int test = currentButtonIndex;
        if (--test < 0)
        {
            currentButtonIndex = SongButtonList.Count - 1;
        }
        else
        {
            currentButtonIndex--;
        }
        MoveButtons();
    }

    public void IncreaseIndex()
    {
        int test = currentButtonIndex;
        if (++test > SongButtonList.Count - 1)
        {
            currentButtonIndex = 0;
        }
        else
        {
            currentButtonIndex++;
        }
        MoveButtons();
    }

    public void MoveButtons()
    {
        Destroy(currentButton);
        string displayname = SongButtonList[currentButtonIndex].GetComponent<SongSelectButton>().DisplayName;
        string artistDisplayName = SongButtonList[currentButtonIndex].GetComponent<SongSelectButton>().ArtistDisplayName;
        GameObject button = Instantiate(ButtonPrefab);
        button.transform.SetParent(ButtonHolder.transform);
        button.transform.localPosition = new Vector3(0, 0, 0);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.name = displayname;
        button.GetComponent<SongSelectButton>().SongDisplayName = displayname;
        button.GetComponent<SongSelectButton>().ArtistDisplayName = artistDisplayName;
        button.GetComponent<SongSelectButton>().Create();
        currentButton = button;
    }


    public void SelectSong(SongSelectButton ssb)
    {
        //SongSelectButton ssb = this.GetComponent<SongSelectButton>();
        MusicName = ssb.MusicName;
        ArtistName = ssb.ArtistDisplayName;
        SongName = ssb.SongDisplayName;
        DisplayName = ssb.DisplayName;
        StepListName = ssb.SteplistName;
        BMP = ssb.BPM;
        FindSong();
        
    }

    public void ActivateCreateMode()
    {
        CreateDropDown();
    }
    public void CreateModeButton()
    {
        CreateModeObject.SetActive(!CreateModeObject.activeSelf);
        InCreateMode = !InCreateMode;
    }

    public void CreateModeStart()
    {
        InCreateMode = true;
        MusicName = songDropdown.captionText.text;
        StepListName = Filename.text;
        ArtistName = "NaN";
        SongName = "NaN";
        DisplayName = Filename.text;
        BMP = System.Convert.ToInt32(BmpField.text);
        FindSong();
        Application.LoadLevel(1);
    }

    public void CreateDropDown()
    {
        foreach (AudioClip auClip in LoadMusic.clips)
        {
            songDropdown.options.Add(new Dropdown.OptionData(auClip.name));
        }
    }

    public void FindSong()
    {
        foreach (AudioClip aClip in LoadMusic.clips)
        {
            Debug.Log(aClip.name);
            if (aClip.name.Equals(MusicName))
            {
                songAudioClip = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
            string testName = MusicName + ".ogg";
            if (aClip.name.Equals(testName)){
                songAudioClip = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
            testName = aClip.name + ".wav";
            if (aClip.name.Equals(testName))
            {
                songAudioClip = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
        }
        if (songAudioClip == null)
        {
            Debug.Log("Music not found. Looking for filename " + MusicName);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
