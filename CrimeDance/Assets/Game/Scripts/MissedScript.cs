using UnityEngine;
using System.Collections;

public class MissedScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Note>() == null)
        {

        }
        else
        {
            GameController.instance.TriggerEventRemove(other.GetComponent<Note>());
            //other.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
    }
}
