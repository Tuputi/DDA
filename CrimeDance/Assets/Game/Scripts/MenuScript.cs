using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;


public class MenuScript : MonoBehaviour {

    public AudioClip song;
    public static string SongName;
    public static string NoteListName;
    public static int BMP;



    public GameObject instructions;
    public static bool InCreateMode = false;
    public static MenuScript instance;

    public InputField Filename;
    public InputField BmpField;
    public Dropdown songDropdown;

    public GameObject ButtonPrefab;
    public GameObject ButtonHolder;
    public GameObject CreateModeObject;

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
            string displayname = song.DisplayName;
            GameObject button = Instantiate(ButtonPrefab);
            button.GetComponent<SongSelectButton>().SongDisplayName = displayname;
            button.name = displayname;
            SongButtonList.Add(button);
        }  
    }

    void Update()
    {
        WatchInput();
    }

    public void WatchInput()
    {
        int test = currentButtonIndex;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            if(--test < 0)
            {
                currentButtonIndex = SongButtonList.Count - 1;           
            }
            else
            {
                currentButtonIndex--;
            }      
            MoveButtons();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            if(++test > SongButtonList.Count - 1)
            {
                currentButtonIndex = 0; 
            }
            else
            {
                currentButtonIndex++;
            }
            MoveButtons();
        }
    }


    public void MoveButtons()
    {
        Destroy(currentButton);
        string displayname = SongButtonList[currentButtonIndex].GetComponent<SongSelectButton>().SongDisplayName;
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


    public void SelectSongName(string songname)
    {
        SongSelectButton ssb = this.GetComponent<SongSelectButton>();
        SongName = ssb.MusicName;
        BMP = ssb.BPM;
        NoteListName = ssb.SteplistName;
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
        SongName = songDropdown.captionText.text;
        NoteListName = Filename.text;
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
            if (aClip.name.Equals(SongName))
            {
                song = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
            string testName = aClip.name + ".ogg";
            if (testName.Equals(SongName)){
                song = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
            testName = aClip.name + ".wav";
            if (testName.Equals(SongName))
            {
                song = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
        }
        if (song == null)
        {
            Debug.Log("song not found. Looking for filename " + SongName);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
