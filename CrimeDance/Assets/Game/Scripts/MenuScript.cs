using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public AudioClip song;
    public static string SongName;
    public static string NoteListName;
    public GameObject instructions;
    public static bool InCreateMode = false;
    public static MenuScript instance;

    public InputField Filename;
    public InputField BmpField;
    public Dropdown songDropdown;
    

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        GameObject.Find("Instructions").GetComponent<UnityEngine.UI.Button>().Select(); //Doesn't activate always on the dancepad?
    }



    void CreateSonglist()
    {
        foreach(AudioClip ac in LoadMusic.clips)
        {
            //add options to songdropdown
        }
    }



    public void SelectSongName(string songname)
    {
        SongName = songname;
        FindSong();
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
        }
        if (song == null)
        {
            Debug.Log("song not found. Looking for filename " + SongName);
        }
    }

    public void SelectNotelist(string notelistname)
    {
        NoteListName = notelistname;
    }

    public void ActivateCreateMode()
    {
        InCreateMode = true;
    }

    public void QuitApplication(){
        Application.Quit();
    }

    public void CreateModeStart()
    {
        InCreateMode = true;
        SongName = songDropdown.captionText.text;
        NoteListName = Filename.text;
        FindSong();
        Application.LoadLevel(1);
    }
}
