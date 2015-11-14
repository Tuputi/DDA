using UnityEngine;
using System.Collections;

public class HitTypeScript : MonoBehaviour {

    public Note.HitType assignHitType;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Note>() == null)
        {

        }
        else
        {
            other.GetComponent<Note>().hitType = assignHitType;
        }
    }
 }
