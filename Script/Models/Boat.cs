using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat
{
    public GameObject gameObject;
    public bool isLeft = true;
    public Role[] roles = new Role[2];
    public void Initialize(Vector3 position){
        gameObject = GameObject.Instantiate(Resources.Load("Prefabs/Boat",typeof(GameObject))) as GameObject;

        gameObject.name = "boat";

        gameObject.transform.position = position;

        roles[0] = roles[1] = null;
        
    }
}
