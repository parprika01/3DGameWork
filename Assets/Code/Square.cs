using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    protected BlackTile black;
    public float[] IniPosition;
    //protected Rigidbody2D rb;
    public bool IsFlash = false;
    public float MoveTime = 0.5F;
    private FlashEffect flashEffect;
    public bool IsMove = false;
    
    protected virtual void Awake(){
    } 

    protected virtual void Start(){
        //获取初始位置
        IniPosition = new float[2];
        IniPosition[0] = transform.position.x;
        IniPosition[1] = transform.position.y;
        
        black = FindObjectOfType<BlackTile>();

        //rb = GetComponent<Rigidbody2D>();

        flashEffect = GetComponent<FlashEffect>();
        if (flashEffect == null)
        {
            Debug.LogError("flashEffect component not found on this GameObject.");
        }
    }

    protected virtual void Update()
    {
        CheckColoring();
    }

    protected bool IsLegalMove(int direction){
        if (IsMove || IsFlash) return false;
        if(IsInBlack() && black.Enable()){
            return false;
        }
        
        if(direction == 1 && Mathf.Abs(transform.position.y - 1) >= 0.2F){   
            if(IsCloseTo() != 1 || !black.Enable() )
                return true;
        }

        if(direction == 2 && Mathf.Abs(transform.position.y + 1) >= 0.2F){
            if(IsCloseTo() != 2 || !black.Enable())
                return true;
        }
            
        if(direction == 3 && Mathf.Abs(transform.position.x + 1) >= 0.2F){
            if(IsCloseTo() != 3 || !black.Enable())
                return true;
        }
            
        if(direction == 4 && Mathf.Abs(transform.position.x - 1) >= 0.2F){
            if(IsCloseTo() != 4 || !black.Enable())
                return true;
        }
        return false;
    }

    protected int IsCloseTo(){
        if(black == null) return 0;
        Vector3 up = new Vector3(0, 1, 0);
        Vector3 down = new Vector3(0, -1, 0);
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        if(Vector2.Distance(transform.position + up,black.transform.position) < 0.5F) return 1;
        if(Vector2.Distance(transform.position + down,black.transform.position) < 0.5F) return 2;
        if(Vector2.Distance(transform.position + left,black.transform.position) < 0.5F) return 3;
        if(Vector2.Distance(transform.position + right,black.transform.position) < 0.5F) return 4;
        return 0;
    }

    protected bool IsInBlack(){
        if(black == null) return false;
        if(black.transform.position == transform.position)
            return true;
        return false;
    }

    protected void CheckColoring(){
        ColorTile[] cTiles = FindObjectsOfType<ColorTile>();
        foreach(ColorTile tile in cTiles){
            if(tile.transform.position == transform.position){
                tile.ChangeColor(GetComponent<SpriteRenderer>().color);
                GameManager.Instance.blackFlag = GameManager.Instance.CheckUnlock();
            }
        }
    }

    public void Hurt(){
        //transform.position = new Vector3(IniPosition[0],IniPosition[1],0);
        StartCoroutine(flashEffect.Blink());
    }

    protected IEnumerator Move(string direction)
    {
        Vector3 movement = new Vector3(0,0,0);
        if(direction == "up") movement = new Vector3(0,1,0);
        if(direction == "down") movement = new Vector3(0,-1,0);
        if(direction =="left") movement = new Vector3(-1,0,0);
        if(direction == "right") movement = new Vector3(1,0,0);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + movement; // 计算目标位置

        float timeElapsed = 0f; // 已经过的时间
        IsMove = true;
        while (timeElapsed < MoveTime && !IsFlash)
        {
            timeElapsed += Time.deltaTime; // 增加经过的时间
            float t = timeElapsed / MoveTime; // 计算比例
            transform.position = Vector3.Lerp(startPosition, targetPosition, t); // 插值计算
            yield return null; // 等待下一帧
        }

        if(!IsFlash) transform.position = targetPosition; // 确保物体最终到达目标位置
        IsMove = false;
    }
}

    
