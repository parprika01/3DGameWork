using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    // Start is called before the first frame update
	private PhysicFlyAction action;
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    #region ISSActionCallback implementation
	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
	}

    public void playDisk(DiskData disk){
		action = PhysicFlyAction.GetSSAction(disk.speed, disk.angle);
		RunAction(disk.gameObject, action, this);
	}
	#endregion
}
