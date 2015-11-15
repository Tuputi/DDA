using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public static AudioClip song;
    public static string SongName;
    public static string NoteListName;
    public GameObject instructions;
    public static bool InCreateMode = false;


    void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject.Find("Instructions").GetComponent<UnityEngine.UI.Button>().Select(); //Doesn't activate always on the dancepad?
    }

	public void SelectSong(AudioClip songA)
    {
        song = songA;
        Debug.Log("Loaf level");
        Application.LoadLevel(1);
    }

    public void SelectSongName(string songname)
    {
        SongName = songname;

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
