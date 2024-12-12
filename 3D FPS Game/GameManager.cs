using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public List<Material> materials;
    private int materialIndex = 0;
    public Game currentGame;
    public static GameManager instance;
    public static GameManager Instance
    {
        get {
            if (instance==null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void EnterGame(Game game){
        Debug.Log("game: " + (game != null));
        currentGame = game;
        Debug.Log("Cgame: " + (game != null));
        currentGame.uiController = FindObjectOfType<UIController>();
        currentGame.scoreController = FindObjectOfType<ScoreController>();
        currentGame.arrowFactory = FindObjectOfType<ArrowFactory>();
        currentGame.Initialize();
    }

    public void ExitGame(){
        currentGame = null;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0){
            if(scroll > 0){
                materialIndex--;
                materialIndex = (materials.Count + materialIndex) % materials.Count;
            } else {
                materialIndex++;
                materialIndex = (materials.Count + materialIndex) % materials.Count;                
            }
            RenderSettings.skybox = materials[materialIndex];
        }
    }
}
