using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public int level = 0;
    public bool IsWin = false;
    public GUIStyle LevelStyle;
    public GUIStyle GameName;
    public Texture2D OperatorGuide;

    public static UI Instance { get; private set; }

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
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI () 
    {   
        string gameName = "Walk To Green";
        if(IsWin)
            gameName = "Congratulations";
        string text = "Level " + (level+1);
        float w = Screen.width ;
        float h = Screen.height;
        float perl = Screen.width / 7;
        GUI.Label(new Rect(0.7f *w, 0.25f * h, perl, perl), text, LevelStyle);
        GUI.Label (new Rect (0.7f *w, 0.15f * h, perl, perl), gameName, GameName);
        GUI.DrawTexture(new Rect(0.6f *w, 0.45f *h, OperatorGuide.width*0.75f, OperatorGuide.height*0.75f), OperatorGuide);
    }
}
