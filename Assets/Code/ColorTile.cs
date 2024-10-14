using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTile : MonoBehaviour
{
    // Start is called before the first frame update
    private Color originalColor;
    public Color currentColor;
    public float alpha = 0.8f;
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        currentColor = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color newColor)
    {
        currentColor = newColor;
        Color tcolor = newColor;
        tcolor.a = alpha;
        GetComponent<SpriteRenderer>().color = tcolor;
    }
}
