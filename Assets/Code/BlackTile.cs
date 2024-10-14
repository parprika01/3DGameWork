using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTile : MonoBehaviour 
{
    // Start is called before the first frame update
    private bool enable = true;
    public Square player;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int IsCloseTo(Square s){
        Vector3 up = new Vector3(0, -1, 0);
        Vector3 down = new Vector3(0, 1, 0);
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        if(transform.position + up == s.transform.position) return 1;
        if(transform.position + down == s.transform.position) return 2;
        if(transform.position + left == s.transform.position) return 3;
        if(transform.position + right == s.transform.position) return 4;
        return 0;
    }

    public bool Enable(){
        return enable;
    }

    public void Lock(){
        enable = true;
    }

    public void Unlock(){
        enable = false;
        spriteRenderer.enabled = false;
    }
}
