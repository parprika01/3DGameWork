using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private ColorTile[] cTiles;
    public BlackTile[] bTiles;
    public Color playerColor;
    public UI ui;
  
    public bool blackFlag = false;
    public bool winFlag = false;

    

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // 如果实例已经存在且不是当前实例，则销毁当前对象
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置单例实例
        Instance = this;

        // 保持这个对象在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        blackFlag = false;
        winFlag = false;
        ui = FindObjectOfType<UI>();
        ui.level = SceneManager.GetActiveScene().buildIndex;
        cTiles = FindObjectsOfType<ColorTile>();
        bTiles = FindObjectsOfType<BlackTile>();
        foreach (BlackTile btile in bTiles){
            btile.Lock();
        }
        Debug.Log("Scene loaded: " + scene.name);
    }

    void Start()
    {
        
    }

    void Update()
    {
       if(blackFlag){
            foreach(BlackTile tile in bTiles){
                tile.Unlock();
            }
       }

       if(winFlag){
            LoadNextScene();
       }
    }

    public bool CheckUnlock(){
        foreach(ColorTile tile in cTiles){
            if(tile.currentColor != playerColor)
                return false;
        }
        return true;
    }

    public void LoadNextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            ui.IsWin = true;
            Debug.Log("没有更多的关卡了");
        }
    }
}
