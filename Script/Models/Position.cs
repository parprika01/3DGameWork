using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    public static Vector3 leftShore = new Vector3(10,0,0);
    public static Vector3 rightShore = new Vector3(-10,0,0);
    public static Vector3 river = new Vector3(0,-0.5f,0);
    public static Vector3 leftBoat = new Vector3(-1.8f,1,0);
    public static Vector3 rightBoat = new Vector3(1.8f,1,0);


    public static Vector3[] RroleShore = new Vector3[] {new Vector3(4.8f,2.5f,0), new Vector3(6.4f,2.5f,0), new Vector3(8f,2.5f,0), new Vector3(9.6f,2.5f,0), new Vector3(11.2f,2.5f,0), new Vector3(12.8f,2.5f,0)};
    public static Vector3[] LroleShore = new Vector3[] {new Vector3(-4.8f,2.5f,0), new Vector3(-6.4f,2.5f,0), new Vector3(-8f,2.5f,0), new Vector3(-9.6f,2.5f,0), new Vector3(-11.2f,2.5f,0), new Vector3(-12.8f,2.5f,0)};
    public static Vector3[] roleBoat = new Vector3[] {new Vector3(-1f,0.5f,0), new Vector3(1f,0.5f,0)};
}