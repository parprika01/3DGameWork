using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imitate : Square
{
    // Start is called before the first frame update
    public Player player;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        IsClission();
    }

    public void ImitateMove(string direction){
        if(direction == "up" && IsLegalMove(1))  StartCoroutine(Move("up"));
        if(direction == "down" && IsLegalMove(2)) StartCoroutine(Move("down")); 
        if(direction == "left" && IsLegalMove(3)) StartCoroutine(Move("left"));  
        if(direction == "right" && IsLegalMove(4)) StartCoroutine(Move("right")); 
    }

    public void IsClission(){
        if(Vector2.Distance(player.transform.position, transform.position) < 0.3F){
            Hurt();
            player.Hurt();
        }
    }
}
