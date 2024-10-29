using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River{
    public GameObject gameObject;
    public void Initialize(Vector3 position){
        gameObject = GameObject.Instantiate(Resources.Load("Prefabs/River",typeof(GameObject))) as GameObject;

        gameObject.name = "river";
        
        gameObject.transform.position = position;
    }
}