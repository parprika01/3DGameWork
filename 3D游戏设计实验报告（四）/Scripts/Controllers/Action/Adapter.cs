using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : MonoBehaviour
{
    // Start is called before the first frame update
    public CCActionManager actionManager;
    public PhysicActionManager physicActionManager;
    private FirstController sceneController; 
    void Start()
    {
        actionManager = gameObject.AddComponent<CCActionManager>();
        physicActionManager = gameObject.AddComponent<PhysicActionManager>();
        
        sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager = this;
    }

    public void playDisk(DiskData disk, bool mode){
        if(mode){
            actionManager.playDisk(disk);
        } else {
            physicActionManager.playDisk(disk);
        }
    }
    void Update()
    {
        
    }
}
