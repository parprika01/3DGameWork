using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role
{
    public GameObject gameObject;
    public bool isPriest;
    public bool isLeft = true;
    public bool isOnShore = true;
    public static int num = 0;
    public int id;
    public void Initialize(bool isPriest, Vector3 position){
        id = ++num; 
        if (isPriest)
            gameObject = GameObject.Instantiate(Resources.Load("Prefabs/Priest",typeof(GameObject))) as GameObject;
        else
            gameObject = GameObject.Instantiate(Resources.Load("Prefabs/Devil",typeof(GameObject))) as GameObject;
        
        gameObject.name = "role" + id;

        gameObject.transform.position = position;

        this.isPriest = isPriest;
    }


}
