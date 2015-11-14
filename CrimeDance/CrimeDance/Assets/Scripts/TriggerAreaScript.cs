using UnityEngine;
using System.Collections;

public class TriggerAreaScript : MonoBehaviour {

	
    //public static bool OnTriggerArea = false;
    public Note currentNote;


   


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Note>() == null)
        {

        }
        else
        {

            currentNote = other.GetComponent<Note>();
            //OnTriggerArea = true;
            GameController.instance.TriggerEventAdd(this);
            //other.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       /* if (other.GetComponent<Note>() == null)
        {

        }
        else
        {
            //  OnTriggerArea = false;
           GameController.instance.TriggerEventRemove(this);

            // other.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }*/

    }
}
