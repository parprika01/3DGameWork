using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    // private FirstPersonController firstPersonController;
    public Game game;
    private GameManager gameManager;
    private GameObject player;
    private bool hasTriggerd = false;
    private bool isOnSpot = false;
    Vector3 IniPosition;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTriggerd) return;
        
        // 当进入范围时，可选择进入射击模式
        if(hasTriggerd && !isOnSpot){
            if(Input.GetKeyDown(KeyCode.F)){
                isOnSpot = true;
                gameManager.EnterGame(game);
            }
        } else if (isOnSpot){
            if(Input.GetKeyDown(KeyCode.F)){
                isOnSpot = false;
                gameManager.ExitGame();
            }            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(!hasTriggerd){
            if(other.gameObject.tag == "Player"){
                hasTriggerd = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        gameManager.ExitGame();
        isOnSpot = false;
        hasTriggerd = false;
    }

    // 将Player固定在Spot上
    private void MovePlayer(GameObject player){
        FirstPersonController firstPersonController= player.GetComponent<FirstPersonController>();
        player.transform.position = transform.position + new Vector3(0, 0.1f, 0);
        firstPersonController.enableJump = false;
        firstPersonController.playerCanMove = false;
        isOnSpot = true;
    }

    private void ReMovePlayer(GameObject player){
        FirstPersonController firstPersonController= player.GetComponent<FirstPersonController>();
        firstPersonController.enableJump = true;
        firstPersonController.playerCanMove = true;
        isOnSpot = false;
    }
}