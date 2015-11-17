using UnityEngine;
using System.Collections;

public class SongSelectButton : MonoBehaviour {

    public string MusicName;
    public string SteplistName;
    public int BPM;
    public string SongDisplayName;
    public string ArtistDisplayName;


    void Awake(){

        Create();

    }

    public void Create(){
        this.gameObject.transform.FindChild("ArtistName").GetComponent<UnityEngine.UI.Text>().text = ArtistDisplayName;
        this.gameObject.transform.FindChild("SongName").GetComponent<UnityEngine.UI.Text>().text = SongDisplayName;
    }
}
