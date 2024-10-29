using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction sceneController;
    public GUIStyle customButton;
    public GUIStyle customBox;
    public GUIStyle customTimer;
    public GameObject winNotice, gameOverNotice;
    String ButtonName1 = "Start";
    String ButtonName2 = "Pause";
    
    void Start(){
        sceneController = SSDirector.getInstance().currentSceneController as IUserAction;
        winNotice.SetActive(false);
        gameOverNotice.SetActive(false);
    }

    void OnGUI()
    {
        GUI.Label (new Rect (20,20,100,40), "RemainTime: " + sceneController.GetRemainTime(),customTimer);
        GUI.Box(new Rect(20,60,200,180), "Loader Menu", customBox);

        if(GUI.Button(new Rect(20,90,160,40), ButtonName1, customButton))
        {
            if (ButtonName1 == "Start") {
                sceneController.GameStart();
                ButtonName1 = "ReStart";
            } else {
                sceneController.GameRestart();
                ButtonName1 = "Start";
            }
        }

        if(GUI.Button(new Rect(20,150,160,40), ButtonName2, customButton)) 
        {
            if (ButtonName2 == "Pause") {
                sceneController.Pause();
                ButtonName2 = "Resume";
            } else {
                sceneController.Resume();
                ButtonName2 = "Pause";
            }
        }
        
        if(sceneController.GameOver())
        {
            print("GameOver");
            gameOverNotice.SetActive(true);
        }

        if(sceneController.Win())
        {
            print("Win");
            winNotice.SetActive(true);
        }

    }
}
