using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerTest : MonoBehaviour {

    private bool d_pressed = false;
    private bool u_pressed = false;
    private bool l_pressed = false;
    private bool r_pressed = false;
   
    
	void Update () {

        if (GameController.instance.createMode)
        {
            if (Input.GetMouseButton(0))
            {
                GameController.instance.noteTypeMode = NoteType.golden;
                GameController.instance.createModeSign.color = Color.yellow;
            }
            if (Input.GetMouseButton(1))
            {
                GameController.instance.noteTypeMode = NoteType.action;
                GameController.instance.createModeSign.color = Color.red;
            }
            if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
            {
                GameController.instance.noteTypeMode = NoteType.normal;
                GameController.instance.createModeSign.color = Color.white;
            }
        }



        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter");
            GameController.instance.RegisterButtonPress(Direction.Up);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Up);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Up);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Down);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Down);
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Left);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Left);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Right);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Right);
            }

        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameController.instance.createMode)
            {
                Song song = GameObject.Find("Song").GetComponent<Song>();
                List<Note> notes = new List<Note>();
                for (int i = 0; i < song.transform.childCount; i++)
                {
                    notes.Add(song.transform.GetChild(i).GetComponent<Note>());
                }
                SaveLoad.SongSaveLoad.Save(SaveLoad.SongSaveLoad.CreateSongXml(MenuScript.NoteListName, 200, notes), MenuScript.NoteListName);
            }
            /* Debug.Log("Space");
             Song song = GameObject.Find("Song").GetComponent<Song>();
             List<Note> notes = new List<Note>();
             for(int i = 0; i < song.transform.childCount; i++)
             {
                 notes.Add(song.transform.GetChild(i).GetComponent<Note>());
             }
             SaveLoad.SongSaveLoad.Save(SaveLoad.SongSaveLoad.CreateSongXml("Cool_Cat_new", 200, notes), "Cool_cat_new");*/

        }







        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            // Debug.Log("Button A");
            // GameController.instance.RegisterButtonPress(ButtonType.UR);
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Up);
            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Up);
            }
             

            //
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //  Debug.Log("Button B");
            //GameController.instance.RegisterButtonPress(ButtonType.UL);
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Left);
                
            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Left);
            }
            
           // 

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            //  Debug.Log("Button X");
            //GameController.instance.RegisterButtonPress(ButtonType.DR);
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Right);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Right);
            }
        
           // 

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //  Debug.Log("Button Y");
            // GameController.instance.RegisterButtonPress(ButtonType.DL);
            if (GameController.instance.createMode)
            {
                GameController.instance.CreateNote(Direction.Down);

            }
            else
            {
                GameController.instance.RegisterButtonPress(Direction.Down);
            }
            
            //

        }


        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("Button 4");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("Button5");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Debug.Log("Back");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("start");
            if (GameController.instance.createMode)
            {
                Song song = GameObject.Find("Song").GetComponent<Song>();
                List<Note> notes = new List<Note>();
                for (int i = 0; i < song.transform.childCount; i++)
                {
                    notes.Add(song.transform.GetChild(i).GetComponent<Note>());
                }
                SaveLoad.SongSaveLoad.Save(SaveLoad.SongSaveLoad.CreateSongXml(MenuScript.NoteListName, 200, notes), MenuScript.NoteListName);
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            Debug.Log("8");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Debug.Log("9");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button10))
        {
            Debug.Log("10");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            Debug.Log("11");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button12))
        {
            Debug.Log("12");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button13))
        {
            Debug.Log("13");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button14))
        {
            Debug.Log("14");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button15))
        {
            Debug.Log("15");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button16))
        {
            Debug.Log("16");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button17))
        {
            Debug.Log("17");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button18))
        {
            Debug.Log("18");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button19))
        {
            Debug.Log("19");
        }

        if (Mathf.Approximately(Input.GetAxisRaw("Horizontal"), -1) && !r_pressed)
        {
            Debug.Log("Right");
            r_pressed = true;
            //GameController.instance.RegisterButtonPress(ButtonType.R);
        }
        else if (Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 1) && !l_pressed)
        {
            Debug.Log("left");
            l_pressed = true;
            //GameController.instance.RegisterButtonPress(ButtonType.L);
        }
        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            r_pressed = false;
            l_pressed = false;
        }

        if (Mathf.Approximately(Input.GetAxisRaw("Vertical"), -1) && !u_pressed)
        {
            Debug.Log("up");
            u_pressed = true;
            //GameController.instance.RegisterButtonPress(ButtonType.U);
        }
        else if (Mathf.Approximately(Input.GetAxisRaw("Vertical"), 1) && !d_pressed)
        {
            Debug.Log("down");
            d_pressed = true;
            //GameController.instance.RegisterButtonPress(ButtonType.D);
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            u_pressed = false;
            d_pressed = false;
        }

        if(u_pressed && l_pressed)
        {
            Debug.Log("left and right");
        }

    }
}
