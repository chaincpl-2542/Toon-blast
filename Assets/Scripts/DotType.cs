using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotType : MonoBehaviour
{
    public DotColor dotColor;
    SpriteRenderer spriteRenderer;
    
    public enum DotColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int num = Random.Range(0,4);
        switch (num)
        {
            case 0:
                dotColor = DotColor.Red;
                break;

            case 1:
                dotColor = DotColor.Green;
                break;

            case 2:
                dotColor = DotColor.Blue;
                break;

            case 3:
                dotColor = DotColor.Yellow;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (dotColor)
        {
            case DotColor.Red:
                Color redColor = ColorHelper.HexToColor("#F89393");
                spriteRenderer.color = redColor;
                break;

            case DotColor.Green:
                Color greenColor = ColorHelper.HexToColor("#AAF18C");
                spriteRenderer.color = greenColor;
                break;

            case DotColor.Blue:
                Color blueColor = ColorHelper.HexToColor("#B4E9FF");
                spriteRenderer.color = blueColor;
                break;

            case DotColor.Yellow:
                Color yellowColor = ColorHelper.HexToColor("#F3ED60");
                spriteRenderer.color = yellowColor;
                break;
        }
    }
}
