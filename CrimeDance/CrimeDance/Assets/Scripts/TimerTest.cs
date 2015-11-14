using UnityEngine;
using System.Collections;

public class TimerTest : MonoBehaviour {

    int time = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        double time = MusicSheetMover.instance.currentTime;
        Debug.Log("Current time is: "+MusicSheetMover.instance.currentTime);
        
    }


    void OnTriggerExit2D(Collider2D other)
    {
        double tim2 = MusicSheetMover.instance.currentTime;
        double temp = System.Math.Round((time + tim2) / 2, 1);
        var decPlaces = (int)(((decimal)temp % 1) * 100);
        int tempInt = (int)temp;
        if (decPlaces <= 2)
        {   
            MusicSheetMover.TimingAdjustment = tempInt;
        }
        else if(decPlaces <= 7)
        {
            MusicSheetMover.TimingAdjustment = tempInt + 0.5;
        }
        else
        {
            MusicSheetMover.TimingAdjustment = tempInt + 1;
        }
        

        GameController.gameStart = true;
        Debug.Log("Adjustment is: "+MusicSheetMover.TimingAdjustment);
        GameController.instance.TimerTestCompleted = true;
        Destroy(this.gameObject);
    }
}
