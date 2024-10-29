using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
	
	private FirstController sceneController;
	private CCMoveToAction move;
	private CCSequenceAction move1;
	public float speed = 4f;

	protected new void Start() {
		
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager = this;
	}

	// Update is called once per frame
	protected new void Update ()
	{
		base.Update ();
		if (sceneController.isRun && Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				GameObject clickedObject = hit.collider.gameObject;

				// 如果点击船,船不为空时可移动
				if (sceneController.boat.gameObject == clickedObject){
					if (sceneController.dataManager.IsBoatEmpty()) {
						Vector3 target = sceneController.dataManager.MoveBoat();
						move = CCMoveToAction.GetSSAction(target,speed);
						RunAction(sceneController.boat.gameObject,move,this);
						for (int i = 0; i < 2; i++) {
							Role cur = sceneController.boat.roles[i];
							if (cur != null) {
								move = CCMoveToAction.GetSSAction(target + Position.roleBoat[i],speed);
								RunAction(cur.gameObject,move,this);
							}
						}
					}
				} 
				// 如果点击角色
				for (int i = 0; i < 6; i++) {
					if (sceneController.roles[i].gameObject != clickedObject) continue;
					if (sceneController.roles[i].isOnShore) {
						move1 = GetMoveAction(sceneController.dataManager.GoToBoat(ref sceneController.roles[i]),sceneController.roles[i].gameObject.transform.position,speed);
					} else {
						move1 = GetMoveAction(sceneController.dataManager.LeaveBoat(ref sceneController.roles[i]),sceneController.roles[i].gameObject.transform.position,speed);
					}
					RunAction(sceneController.roles[i].gameObject,move1,this);
					return;
				}
			}
		}
		
	}
	
	private CCSequenceAction GetMoveAction(Vector3 target, Vector3 cur, float speed) {
		List<SSAction> moves = new List<SSAction>();
		Vector3 node1 = new Vector3(cur.x, 6f, cur.z);
		Vector3 node2 = new Vector3(target.x, 6f, target.z);
		moves.Add(CCMoveToAction.GetSSAction(node1,speed));
		moves.Add(CCMoveToAction.GetSSAction(node2,speed));
		moves.Add(CCMoveToAction.GetSSAction(target,speed));
		return CCSequenceAction.GetSSAction(1,0,moves);
	}

	#region ISSActionCallback implementation
	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
	}
	#endregion
}

