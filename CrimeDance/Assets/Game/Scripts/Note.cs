using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

    public double PlayTime;
    public Direction direction;
    public int Row = 1;
    public bool isGolden = false;


    public enum HitType { perfect, good, poor, miss, none };
    public HitType hitType = HitType.none;

    public NoteType NoteType = NoteType.normal;

    void Awake(){
        Sprite arrow = null;

        if (NoteType == NoteType.action)
        {
            arrow = PrefabHolder.instance.ACTION;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }
        if (NoteType == NoteType.actionGUN)
        {
            arrow = PrefabHolder.instance.ACTION_gun;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }
        if (NoteType == NoteType.actionESCAPE)
        {
            arrow = PrefabHolder.instance.ACTION_escape;
            this.GetComponent<UnityEngine.UI.Image>().sprite = arrow;
            return;
        }

        switch (direction)
        {
            case Direction.Up:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.U_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.U;
                }
                break;
            case Direction.UR:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.UR_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.UR;
                }
                break;
            case Direction.Right:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.R_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.R;
                }
                break;
            case Direction.DR:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.DR_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.DR;
                }
                break;
            case Direction.Down:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.D_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.D;
                }
                break;
            case Direction.DL:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.DL_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.DL;
                }
                break;
            case Direction.Left:
                if (NoteType == NoteType.golden)
                {
                    arrow = PrefabHolder.instance.L_G;
                }
                else
                {
                    arrow = PrefabHolder.instance.L;
                }
                break;
            case Direction.UL:
                if (NoteType == NoteType.golden)
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
