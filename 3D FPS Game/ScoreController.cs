using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int Score{get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int UpdateScore(GameObject ring){
        if(ring == null) {
            Score += 30;
            print(Score);
            return Score;
        }
        switch(ring.name){
            case "Edge":
                Score += 20;
                break;
            case "Middle":
                Score += 50;
                break;
            case "Center":
                Score += 100;
                break;
            case "animal":
                Score -= 75;
                break;
        }
        return Score;
    }

    public void Initialize(){
        Score = 0;
    }
}
