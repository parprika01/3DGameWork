using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shore
{
    public GameObject gameObject;
    public Role[] roles = new Role[6];
    static int num = 0;
    public void Initialize(Vector3 position){
        num++;
        gameObject = GameObject.Instantiate(Resources.Load("Prefabs/Shore",typeof(GameObject))) as GameObject;

        gameObject.name = "shore" + num;
        
        gameObject.transform.position = position;
    }
}
