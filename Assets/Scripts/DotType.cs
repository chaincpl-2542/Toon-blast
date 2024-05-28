using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using UnityEngine;

public class DotType : MonoBehaviour
{
    public DotColor dotColor;
    public Special specialDot;
    public bool checkDestory;
    public MySlot currentSlot;
    SpriteRenderer spriteRenderer;

    public enum DotColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    public enum Special
    {
        Normal,
        Disco,
        CrossBomb
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       
        if(specialDot == Special.Normal)
        {
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
    }

    // Update is called once per frame
    void Update()
    {
        if(specialDot == Special.Normal)
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
    public void DotDestory()
    {
        checkDestory = true;
        
        if(specialDot == DotType.Special.Normal)
        {
            Destroy(gameObject);
        }
        else if(specialDot == DotType.Special.CrossBomb)
        {
            Destroy(gameObject);
        }
        else if(specialDot == DotType.Special.Disco)
        {
            Destroy(gameObject);
        }
    }
}
