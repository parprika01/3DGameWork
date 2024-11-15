using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager{
	
	private FirstController sceneController;
	CCFlyAction action;

	protected  void Start() {
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		base.Start();
	}

	// Update is called once per frame
	
	protected  void Update ()
	{
		base.Update();
	}
		
	#region ISSActionCallback implementation
	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
	}

	public void playDisk(DiskData disk){
		DataManager.MoveData m = sceneController.dataManager.GetMoveData(disk);

		action = CCFlyAction.GetSSAction(m.speed, m.angle, m.gravity); 
		RunAction(disk.gameObject, action, this);
	}
	#endregion
}

