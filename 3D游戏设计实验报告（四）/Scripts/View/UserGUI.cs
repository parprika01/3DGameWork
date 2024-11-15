using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UserGUI : MonoBehaviour
{
    public FirstController userAction;
    private String ButtonName1 = "Start";
    private String ButtonName2 = "Physis";
    public GUIStyle customButton;
    public GUIStyle customLabel;
    public GameObject catchFood, gameOver;

    private float w;
    private float h;
    private int score;
    private int life;
    private bool IsGameOver;
    // Start is called before the first frame update
    void Start()
    {
        userAction =  (FirstController)SSDirector.getInstance ().currentSceneController;
        userAction.userGUI = this;
        w = Screen.width;
        h = Screen.height;
        gameOver.SetActive(false);
        catchFood.SetActive(true);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (IsGameOver) {
            userAction.GameOver();
            gameOver.SetActive(true);
            catchFood.SetActive(false);
        }

        if (GUI.Button(new Rect(h * 0.02f, h * 0.08f, w * 0.15f, h * 0.065f),ButtonName1,customButton)) {
            if (ButtonName1 == "Start") {
                userAction.GameStart();
                ButtonName1 = "Restart";
            } else {
                userAction.GameRestart();
                ButtonName1 = "Start";
                gameOver.SetActive(false);
                catchFood.SetActive(true);
            }
        }

        if (GUI.Button(new Rect(h * 0.02f, h * 0.18f, w * 0.15f, h * 0.065f),ButtonName2,customButton)) {
            if (ButtonName2 == "Physis") {
                ButtonName2 = "Kinematics";
                userAction.GameModeSet(false);
            } else {
                ButtonName2 = "Physis";
                userAction.GameModeSet(true);
            }
            
        }        
        GUI.Label(new Rect(h * 0.02f, 20, 100, 50),"Score:" + score, customLabel);
        GUI.Label(new Rect(w * 0.15f, 20, 100, 50), "Life:" + life, customLabel);
    }

    public void SetScore(int score){
        this.score = score;
    }

    public void SetLife(int life){
        this.life = life;
    }

    public void SetIsGameOver(bool IsGameOver){
        this.IsGameOver = IsGameOver;
    }
}
