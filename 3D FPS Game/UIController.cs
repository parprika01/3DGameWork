using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  
using UnityEngine;
using System.Reflection.Emit;

public class UIController : MonoBehaviour
{
    public GameObject weaponUI;
    private Image image;
    private List<GameObject> used = new List<GameObject>();
    private List<GameObject> free = new List<GameObject>();
    private int Score;
    private int ArrowNum;
    private int FireArrowNum;
    private string ArrowType;
    // Start is called before the first frame update

    void Start(){
        image = weaponUI.GetComponent<Image>();
    }

    public void ShowHitUI(string name, Vector3 position, Vector3 up, Quaternion rotation){
        GameObject ui = GetHitUI(name, position, up, rotation);
        FreeHitUI(ui);
    }

    public GameObject GetHitUI(string name, Vector3 position, Vector3 up,  Quaternion rotation){
        GameObject ui;
        for(int i = 0 ; i < free.Count ; i++){
            if(free[i].tag == GetUIName(name)){
                ui = free[i];
                used.Add(free[i]);
                free.Remove(free[i]);
                ui.transform.position = position + up * 1f;
                ui.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                ui.SetActive(true);
                return ui;
            }
        }

        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + GetUIName(name));
        ui = Instantiate(prefab, position + up * 1f , prefab.transform.rotation * rotation);
        used.Add(ui);
        return ui;
    }

    public void FreeHitUI(GameObject ui){
        used.Remove(ui);
        free.Add(ui);
        StartCoroutine(RecycleUI(ui));
    }

    
    private IEnumerator RecycleUI(GameObject ui) {
        yield return new WaitForSeconds(0.5f);
        ui.SetActive(false);
    }

    private string GetUIName(string name){
        switch (name){
            case "Edge":
                return "Good";
            case "Middle":
                return "Perfect";
            case "Center":
                return "Excellent";
            case "animal":
                return "Hurt";
            default:
                return "Perfect";
        }
    }

    public void ShowWeapon(string name){
        switch(name){
            case "Arrow":
                image.sprite = Resources.Load<Sprite>("Self/Group 6");
                break;
            case "FireArrow":  
                image.sprite = Resources.Load<Sprite>("Self/FireArrow");
                break;
            case "splatoon":
                break;
        }
        ArrowType = name;
    }
    public void SetScore(int score){
        Score = score;
    }

    public void SetArrowNum(int num){
        ArrowNum = num;
    }

    public void SetFireArrowNum(int num){
        FireArrowNum = num;
    }

    private void OnGUI(){
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.black;
        labelStyle.fontSize = (int)(Screen.width * 0.015);
        if(ArrowType == "Arrow"){
            GUI.Label(new Rect(Screen.width * 0.97f, Screen.height* 0.72f, 100, 100), FireArrowNum.ToString(), labelStyle);
            GUI.Label(new Rect(Screen.width * 0.97f, Screen.height* 0.755f, 100, 100), ArrowNum.ToString(), labelStyle);            
        } else {
            GUI.Label(new Rect(Screen.width * 0.97f, Screen.height* 0.72f, 100, 100), ArrowNum.ToString(), labelStyle);
            GUI.Label(new Rect(Screen.width * 0.97f, Screen.height* 0.755f, 100, 100), FireArrowNum.ToString(), labelStyle);            
        }
        GUI.Label(new Rect(Screen.width * 0.78f, Screen.height* 0.9f, 100, 100), "Score:"+Score.ToString(), labelStyle);

    }
}
