using UnityEngine;
using System.Collections;

public class SongSelectButton : MonoBehaviour {

    [Tooltip("What's the name of the music file?")]
    public string MusicName;
    [Tooltip("What's the filename for the recorded steps?")]
    public string SteplistName;
    [Tooltip("What should the song/level be called in the in-game menu?")]
    public string DisplayName;
    public int BPM;
    [Tooltip("What should the song be called as?")]
    public string SongDisplayName;
    [Tooltip("Artist name?")]
    public string ArtistDisplayName;


    void Awake(){

        Create();

    }

    public void Create(){
        this.gameObject.transform.FindChild("ArtistName").GetComponent<UnityEngine.UI.Text>().text = ArtistDisplayName;
        this.gameObject.transform.FindChild("SongName").GetComponent<UnityEngine.UI.Text>().text = SongDisplayName;
    }


    public void SelectSong()
    {
        //SongSelectButton ssb = this.GetComponent<SongSelectButton>();
        MenuScript.MusicName = this.MusicName;
        MenuScript.ArtistName = this.ArtistDisplayName;
        MenuScript.SongName = this.SongDisplayName;
        MenuScript.DisplayName = this.DisplayName;
        MenuScript.StepListName = this.SteplistName;
        MenuScript.BMP = this.BPM;
        MenuScript.instance.FindSong();

    }

}
