using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private FirstController sceneController;
    public GameObject background;
    private int score = 0; 
    private int live;
    public float interval = 1f;
    public int round = 1;
    public float gravity = -10f; 
    public bool mode = true;
    public struct EdgeData{
        public float Xedge;
        public float Yedge;
    }

    public struct MoveData{
        public float gravity;
        public float speed;
        public float angle;
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
        sceneController.dataManager = this;
        Physics.gravity = new Vector3(0,gravity,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= (int)Mathf.Pow(round, 1.5f) * 200) round++;
    }

    public void Init(){
        score = 0;
        live = 5;
        round = 1;
        sceneController.userGUI.SetIsGameOver(false);
        sceneController.userGUI.SetScore(score);
        sceneController.userGUI.SetLife(live);
    }

    public void UpdateScore(GameObject disk) {
        DiskData diskData = disk.GetComponent<DiskData>();
        score += diskData.score * (int)Math.Sqrt(round);
        interval = 1f - round * 0.1f;
        sceneController.userGUI.SetScore(score);
    }

    public void DeductLife() {
        if (live > 0) live--;
        sceneController.userGUI.SetLife(live);
        if (live == 0) sceneController.userGUI.SetIsGameOver(true);
    }

    public void AddLife() {
        live++;
        sceneController.userGUI.SetLife(live);
    }

    public MoveData GetMoveData(DiskData disk){
        MoveData m;
        m.gravity = gravity;
        m.speed = disk.speed;
        m.angle = disk.angle;
        return m;
    }

    public EdgeData GetEdgeData(){
        EdgeData e;
        e.Xedge = background.transform.localScale.x / 2f;
        e.Yedge = background.transform.localScale.y / 2f;
        return e;
    }

    public bool IsInScene(float x, float y) {
        EdgeData e;
        e.Xedge = background.transform.localScale.x / 2f;
        e.Yedge = background.transform.localScale.y / 2f;
        if (x >= -e.Xedge && x <= e.Xedge && y >= -e.Yedge) return true;
        return false;
    }
}
