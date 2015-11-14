using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

    public double PlayTime;
    public ControllerTest.ButtonType direction;
    public int Row = 1;
    public bool isGolden = false;


    public enum HitType { perfect, good, poor, miss, none };
    public HitType hitType = HitType.none;

    public enum NoteType { normal, golden, action, actionGUN, actionESCAPE}
    public NoteType noteType = NoteType.normal;

    void Awake(){
        Sprite arrow = null;

        if (noteType == Note.NoteType.action)
        {
            arrow = PrefabHolder.instance.ACTION;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }
        if (noteType == Note.NoteType.actionGUN)
        {
            arrow = PrefabHolder.instance.ACTION_gun;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }
        if (noteType == Note.NoteType.actionESCAPE)
        {
            arrow = PrefabHolder.instance.ACTION_escape;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }

        switch (direction)
        {
            case ControllerTest.ButtonType.U:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.U_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.U;
                }
                break;
            case ControllerTest.ButtonType.UR:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.UR_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.UR;
                }
                break;
            case ControllerTest.ButtonType.R:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.R_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.R;
                }
                break;
            case ControllerTest.ButtonType.DR:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.DR_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.DR;
                }
                break;
            case ControllerTest.ButtonType.D:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.D_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.D;
                }
                break;
            case ControllerTest.ButtonType.DL:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.DL_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.DL;
                }
                break;
            case ControllerTest.ButtonType.L:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.L_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.L;
                }
                break;
            case ControllerTest.ButtonType.UL:
                if (noteType == Note.NoteType.golden)
                {
                    arrow = PrefabHolder.instance.UL_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.UL;
                }
                break;
            default:
                break;
        }

        this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
    }

    //Gold note logic

    public bool isStarter = false;
    public bool isEndNote = false;


}
