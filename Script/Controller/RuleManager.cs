using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    private FirstController sceneController;
    private DataManager.shoreInfo pi; 
 
    void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
        sceneController.ruleManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        pi = sceneController.dataManager.GetShoreInfo();
    }

    public bool IsWin() {
        return !IsGameOver() && sceneController.dataManager.getRightPriestNum() >= 3;
    } 

    public bool IsGameOver() {
        if (sceneController.timer <= 0f || pi.leftRoleSub < 0 || pi.rightRoleSub < 0) {
            return true;
        } else return false;
    }
}
