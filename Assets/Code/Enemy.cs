using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Square 
{
    // Start is called before the first frame update
    public int[] movement;
    public float interval = 0.75F;
    public Player player;
    private int index = 0;
    private float timer = 0F;
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        if (timer > interval && !IsFlash && (!IsInBlack() || !black.Enable())){
            Move();
            timer = 0F;
        }

        IsClission();
    }

    public void Move(){
        switch (movement[index]){
            case 1:
                if(IsLegalMove(1))
                    StartCoroutine(Move("up"));
                break;
            case 2:
                if(IsLegalMove(2))
                    StartCoroutine(Move("down"));
                break;
            case 3:
                if(IsLegalMove(3))
                    StartCoroutine(Move("left"));
                break;
            case 4:
                if(IsLegalMove(4))
                    StartCoroutine(Move("right"));
                break;
        }
        index = (index + 1) % movement.Length;
        
            
    }

    public void IsClission(){
        if(Vector2.Distance(player.transform.position, transform.position) < 0.1F){
            index = 0;
            Hurt();
            player.Hurt();
            
        }
    }
}
