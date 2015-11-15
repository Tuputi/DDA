using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrefabHolder : MonoBehaviour {

    public  Sprite U;
    public  Sprite D;
    public  Sprite L;
    public  Sprite R;
    public  Sprite UR;
    public  Sprite DR;
    public  Sprite UL;
    public  Sprite DL;

    public Sprite StarterSprite;
    public Sprite U_G;
    public Sprite D_G;
    public Sprite L_G;
    public Sprite R_G;
    public Sprite UR_G;
    public Sprite DR_G;
    public Sprite UL_G;
    public Sprite DL_G;

    public Sprite ACTION;
    public Sprite ACTION_gun;
    public Sprite ACTION_escape;

    public Text Swanky;
    public Text Good;
    public Text Bad;
    public Text Miss;
    public Text Stealing;
    public GameObject Eliminated;

    public AudioClip snap;
    public AudioClip drum;
    public AudioClip loadGun;
    public AudioClip shoot;

    public static PrefabHolder instance;

    void Awake(){
        instance = this;
    }

}
