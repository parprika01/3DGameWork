using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private FirstController sceneController; 
    private Boat boat;
    private Role[] roles;
	private int[] leftShoreOccupied = new int[6]{1,2,3,4,5,6};
	private int[] rightShoreOccupied = new int[6]{0,0,0,0,0,0};
	public int[] boatOccupied = new int[2]{0,0};
    private posInfo pi;
    private shoreInfo si;
    struct posInfo {
        public Vector3 position;
        public int index;
    }
    public int rightPriestNum = 0;
    public struct shoreInfo {
        public int leftRoleSub;
        public int rightRoleSub;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
        sceneController.dataManager = this;
        pi = new posInfo();
        si= new shoreInfo();
        si.leftRoleSub = 0;
        si.rightRoleSub = 0;
    }

    // Update is called once per frame
    void Update()
    {
        boat = sceneController.boat;
        roles = sceneController.roles;   
    }

    public Vector3 GoToBoat(ref Role role){
        showCondition();
        role.isOnShore = false;
        int index = getRoleShoreIndex(role);
        if (role.isLeft) leftShoreOccupied[index] = 0; 
        else {
            rightShoreOccupied[index] = 0;
            if (role.isPriest) {
                rightPriestNum--;
                si.rightRoleSub--;
            }
        }
        if (boatOccupied[0] == 0 && role.isLeft == boat.isLeft) {
            boatOccupied[0] = role.id;
            sceneController.boat.roles[0] = role; 
            return Position.roleBoat[0] + boat.gameObject.transform.position;
        } else if (boatOccupied[1] == 0  && role.isLeft == boat.isLeft) {
            boatOccupied[1] = role.id;
            sceneController.boat.roles[1] = role; 
            return Position.roleBoat[1] + boat.gameObject.transform.position;
        }
        role.isOnShore = true;
        if (role.isLeft) leftShoreOccupied[index] = role.id; 
        else {
            rightShoreOccupied[index] = role.id;
            if (role.isPriest) {
                rightPriestNum++;
                si.rightRoleSub++;
            }
        }
        
        return role.gameObject.transform.position;
    }
    
    public Vector3 LeaveBoat(ref Role role){
        showCondition();
        role.isOnShore = true;
        bool cur = role.isLeft;
        role.isLeft = boat.isLeft;
        for (int i = 0; i < 2; i++){
            if (boatOccupied[i] == role.id) {
                role.isLeft = boat.isLeft;
                pi = FindShorePosition(boat.isLeft);
                if (pi.index < 6){
                    leftShoreOccupied[pi.index] = role.id;
                    if (role.isPriest) si.leftRoleSub++;
                } 
                else {
                    rightShoreOccupied[pi.index - 6] = role.id;
                    if (role.isPriest) {
                        si.rightRoleSub++;
                        rightPriestNum++;
                        }
                }
                boatOccupied[i] = 0;
                sceneController.boat.roles[i] = null;
                return pi.position;
            }
        }
        role.isOnShore = false;
        role.isLeft = cur;
        return role.gameObject.transform.position;
    }

    public shoreInfo GetShoreInfo(){
        return si;
    } 

    public bool IsBoatEmpty() {
        return boatOccupied[0] != 0 || boatOccupied[1] != 0;
    }

    public Vector3 MoveBoat() {
        sceneController.boat.isLeft = !sceneController.boat.isLeft;
        if (!boat.isLeft) return Position.rightBoat;
        else return Position.leftBoat;
    }

    private posInfo FindShorePosition(bool isLeft){
        if (isLeft) {
            for(int i = 5; i >= 0; i--) {
                if (leftShoreOccupied[i] == 0) {
                    pi.position = Position.LroleShore[i];
                    pi.index = i;
                    return pi;
                }
            }
        } else {
            for(int i = 0; i < 6; i++) {
                if (rightShoreOccupied[i] == 0) {
                    pi.position = Position.RroleShore[i];
                    pi.index = i + 6;
                    return pi;
                }
            }
        }
        return pi;
    }

    void showCondition(){
        print(si.leftRoleSub);
        print(si.rightRoleSub);
    }

    public int getRightPriestNum() {
        return rightPriestNum;
    }

    private int getRoleShoreIndex(Role role){
        if (role.isLeft) {
            for (int i = 0; i < 6; i++) {
                if (leftShoreOccupied[i] == role.id) return i;
            }
        } else {
            for (int i = 0; i < 6; i++) {
                if (rightShoreOccupied[i] == role.id) return i;
            }
        }
        return 0;
    } 
}
