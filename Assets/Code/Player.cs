using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Square
{
    // Start is called before the first frame update
    public GameObject Terminal;
    public Imitate imitate;

    protected override void Awake(){
        base.Awake();
    }

    protected override void Start(){
        base.Start();
        imitate = FindObjectOfType<Imitate>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Controller();
        IsReach();
    }

    public void Controller(){
        if(Input.GetKeyDown(KeyCode.W) && IsLegalMove(1)){
            if(imitate != null)
                imitate.ImitateMove("up");
            StartCoroutine(Move("up"));
        }

        if(Input.GetKeyDown(KeyCode.S) && IsLegalMove(2)){
            if(imitate != null)
                imitate.ImitateMove("down");
            StartCoroutine(Move("down"));
        }

        if(Input.GetKeyDown(KeyCode.A) && IsLegalMove(3)){
            if(imitate != null)
                imitate.ImitateMove("left");
            StartCoroutine(Move("left"));
        }

        if(Input.GetKeyDown(KeyCode.D) && IsLegalMove(4)){
            if(imitate != null)
                imitate.ImitateMove("right");
            StartCoroutine(Move("right"));
        }
    }

    public void IsReach(){
        if(!IsFlash && Vector2.Distance(Terminal.transform.position, transform.position) < 0.1F){
            print("Game Over!");
            GameManager.Instance.winFlag = true;
        }
    }


}
