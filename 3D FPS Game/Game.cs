using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Target[] targets;
    [SerializeField]
    private int ArrowNum;
    [SerializeField]
    private int fireArrowNum;
    public UIController uiController;
    public ScoreController scoreController;
    public GameManager gameManager;
    public ArrowFactory arrowFactory;
    public string ArrowType = "Arrow";
    // Start is called before the first frame update
    void Start()
    {
        // gameManager = FindObjectOfType<GameManager>();
        targets = gameObject.GetComponentsInChildren<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)){
            if (ArrowType == "Arrow"){
                ArrowType = "FireArrow";
            } else {
                ArrowType = "Arrow";
            }
            uiController.ShowWeapon(ArrowType);

        }
    }

    public void Initialize(){
        // 初始化靶子
        foreach (Target target in targets){
            if (target.type == "motivate")
                target.Initialize();
        }
        // 初始化分数
        scoreController.Initialize();

        // 初始化状态
        ArrowNum = 10;
        fireArrowNum = 10;
        uiController.SetArrowNum(ArrowNum);
        uiController.SetFireArrowNum(fireArrowNum);
        uiController.SetScore(0);
    }

    public void UpdateArrowNum(){
        if(ArrowType == "FireArrow"){
            fireArrowNum--;
            uiController.SetFireArrowNum(fireArrowNum);
        } else {
            ArrowNum--;
            uiController.SetArrowNum(ArrowNum);
        }
    }

    public bool IsArrowRunOut(){
        if(ArrowType == "FireArrow")
            return  fireArrowNum == 0;
        else 
            return ArrowNum == 0;
    }
}
