using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public Adapter actionManager { get; set;}
	public DiskFactory diskFactory{ get; set; }
	public DataManager dataManager{ get; set; }
	public UserGUI userGUI{ get; set; }
	public List<GameObject> flyingDisks = new List<GameObject>(); 
	private float timer = 0f;
	public bool IsRunning = false;

	// the first scripts
	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		Debug.Log ("awake FirstController!");
	}
	 
	// loading resources for first scence
	public void LoadResources () {
		;
	}

	public void Pause ()
	{
		throw new System.NotImplementedException ();
	}

	public void Resume ()
	{
		throw new System.NotImplementedException ();
	}

	#region IUserAction implementation
	public void GameOver ()
	{
		SSDirector.getInstance ().NextScene ();
		IsRunning = false;
	}

	public void GameStart() {
		IsRunning = true;
		dataManager.Init();
	}

	public void GameRestart() {
		IsRunning = true;
		dataManager.Init();
	}

	public void GameModeSet(bool mode){
		dataManager.mode = mode;
	}
	#endregion


	// Use this for initialization
	void Start () {
		//give advice first
	}
	
	// Update is called once per frame
	void Update () {
		if (IsRunning) {
			if(Input.GetMouseButton(0)){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit)){
					//划出射线，只有在scene视图中才能看到
					Debug.DrawLine(ray.origin,hit.point);
					GameObject gameObject = hit.collider.gameObject;
					if(gameObject.GetComponent<DiskData>().foodName == "Ribs") dataManager.AddLife();
					dataManager.UpdateScore(gameObject);
					diskFactory.FreeDisk(gameObject);
				}
			}

			timer += Time.deltaTime;
			if (timer >= dataManager.interval) {
				timer = 0f;
				GameObject disk = diskFactory.GetDisk(dataManager.round);
				actionManager.playDisk(disk.GetComponent<DiskData>(), dataManager.mode);
			}
			
		}
		
	}
}
