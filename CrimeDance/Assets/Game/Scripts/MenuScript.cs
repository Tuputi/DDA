using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public AudioClip song;
    public static string SongName;
    public static string NoteListName;
    public GameObject instructions;
    public static bool InCreateMode = false;
    public static MenuScript instance;


    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        GameObject.Find("Instructions").GetComponent<UnityEngine.UI.Button>().Select(); //Doesn't activate always on the dancepad?
    }

	public void SelectSong(AudioClip songA)
    {
        song = songA;
        Debug.Log("Load level");
       
    }

    public void SelectSongName(string songname)
    {
        SongName = songname;
      foreach(AudioClip aClip in LoadMusic.clips)
        {
            Debug.Log(aClip.name);
            if (aClip.name.Equals(songname))
            {
                song = aClip;
                Debug.Log("Song found");
                Application.LoadLevel(1);
            }
        }
      if(song == null)
        {
            Debug.Log("song not found. Looking for filename "+songname);
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
}
