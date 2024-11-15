using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicFlyAction : SSAction
{
    private Vector3 XSpeed;
	private Vector3 YSpeed;
	private Rigidbody rb;
	private DataManager dataManager;
	private DiskFactory diskFactory;
    public static PhysicFlyAction GetSSAction(float Speed, float Angle){
		PhysicFlyAction action = ScriptableObject.CreateInstance<PhysicFlyAction> ();
		action.XSpeed = Speed * Mathf.Cos(Angle / 180 * Mathf.PI) * new Vector3(1, 0, 0);
		action.YSpeed = Speed * Mathf.Sin(Angle / 180 * Mathf.PI) * new Vector3(0, 1, 0);
		return action;
	}

	public override void Update ()
	{
		if (!dataManager.IsInScene(transform.position.x, transform.position.y) && gameobject.activeSelf) {
			//waiting for destroy
			diskFactory.FreeDisk(gameobject);
			dataManager.DeductLife();
			this.destory = true;  
			this.callback.SSActionEvent (this);
		}
	}

	public override void Start () {
		FirstController sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		dataManager = sceneController.dataManager;
		diskFactory = sceneController.diskFactory;
		gameobject.AddComponent<Rigidbody>();
		rb = gameobject.GetComponent<Rigidbody>();
		rb.velocity = XSpeed + YSpeed;
	}
}
