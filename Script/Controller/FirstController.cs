using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public CCActionManager actionManager { get; set;}
	public RuleManager ruleManager { get; set;}
	public DataManager dataManager{ get; set;}
	// public GameObject move1,move2;
	public Boat boat = new Boat();
	public Shore[] shores = new Shore[2];
	public River river = new River();
	public Role[] roles = new Role[6];
	public bool isRun = false;
	public float timer = 60.0f;
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
		boat.Initialize(Position.leftBoat);
		shores[0] = new Shore();
		shores[1] = new Shore();
		shores[0].Initialize(Position.leftShore);
		shores[1].Initialize(Position.rightShore);
		river.Initialize(Position.river);
		for(int i = 0; i < roles.Length; i++){
			roles[i] = new Role();
			if(i < 3)
				roles[i].Initialize(true,Position.LroleShore[i]);
			else
				roles[i].Initialize(false,Position.LroleShore[i]);
		}
	}

	#region IUserAction implementation
	public bool GameOver ()
	{
		if (ruleManager.IsGameOver()) {
			isRun = false;
			return true;
		} else return false;
	}
	public void Pause ()
	{
		// throw new System.NotImplementedException ();
		isRun = false;
	}

	public void Resume ()
	{
		// throw new System.NotImplementedException ();
		isRun = true;
	}

	public bool Win()
	{
		if (ruleManager.IsWin()) {
			isRun = false;
			return true;
		} else return false;
	}

	public void GameStart()
	{
		isRun = true;
	}

	public void GameRestart()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.name);
	}

	public int GetRemainTime()
	{
		return (int)timer;
	}
	#endregion


	void Start () {
		
	}
	
	void Update () {
		if(isRun) {
			timer -= Time.deltaTime;
		}
		if(timer <= 0) {
			isRun = false;
		}
	}

}
