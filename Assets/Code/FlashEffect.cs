using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public float blinkDuration = 0.75F;
    public int blinkCount = 2;
    private Color Inicolor;
    private Color ShinyColor;
    private Square square;

    void Start (){
        Inicolor = GetComponent<SpriteRenderer>().color;
        square = GetComponent<Square>();
        ShinyColor = Inicolor;
        ShinyColor.a = 0;
    }

    public IEnumerator Blink()
    {
        square.IsFlash = true;
        square.transform.position = new Vector3(square.IniPosition[0],square.IniPosition[1],0);
        float blinkInterval = blinkDuration / (blinkCount * 2);
        
        for (int i = 0; i < blinkCount; i++)
        {
            // 切换物体颜色
            GetComponent<SpriteRenderer>().color = ShinyColor;  // 闪烁时的颜色
            yield return new WaitForSeconds(blinkInterval);

            // 恢复到原始颜色
            GetComponent<SpriteRenderer>().color = Inicolor;
            yield return new WaitForSeconds(blinkInterval);
        }
        square.IsFlash = false;
    }

    
}
