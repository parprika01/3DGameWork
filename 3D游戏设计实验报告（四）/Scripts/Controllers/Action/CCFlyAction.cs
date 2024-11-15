using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CCFlyAction : SSAction
{
	public float Power = 10f;
	public float Angle = 45f;
	public float Gravity = -10f;
	private Vector3 XSpeed; 
	private Vector3 YSpeed;
	private DataManager dataManager;
	private DiskFactory diskFactory;

	
	public static CCFlyAction GetSSAction(float Power, float Angle, float Gravity){
		CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction> ();
		action.Power = Power;
		action.Angle = Angle;
		action.Gravity = Gravity; 
		action.XSpeed = new Vector3(1,0,0) * Mathf.Cos(action.Angle / 180 * Mathf.PI) * action.Power;
		action.YSpeed = new Vector3(0,1,0) * Mathf.Sin(action.Angle / 180 * Mathf.PI) * action.Power;
		return action;
	}

	public override void Update ()
	{ 
		transform.position += (XSpeed + YSpeed) * Time.deltaTime;
		YSpeed += Gravity * new Vector3(0,1,0) * Time.deltaTime;
		if (!dataManager.IsInScene(transform.position.x, transform.position.y)  && gameobject.activeSelf) {
			//waiting for destroy
			this.destory = true;  
			this.callback.SSActionEvent (this);
			dataManager.DeductLife();
			diskFactory.FreeDisk(gameobject);
		}
	}

	public override void Start () { 
		FirstController sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		dataManager = sceneController.dataManager;
		diskFactory = sceneController.diskFactory;
	}
}