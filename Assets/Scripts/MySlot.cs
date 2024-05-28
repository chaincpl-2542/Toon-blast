using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySlot : MonoBehaviour
{
    public Vector2 slotPosition;
    public DotType dotType;
    GameManager gameManager;

    public MySlot topSlot;
    public MySlot leftSlot;
    public MySlot rightSlot;
    public MySlot bottomSlot;
    public bool canDestory;
    public List<GameObject> allSlotSameColor;
    public int dotGroupCount;
    bool countCheck;
    float speed = 5.0f;

    private void Start() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update() 
    {
        if((int)slotPosition.y + 1 < gameManager.gridSize.y)
        {
            topSlot = gameManager.allRows[(int)slotPosition.y + 1].mySlots[(int)slotPosition.x];
        }

        if((int)slotPosition.y - 1 >= 0)
        {
            bottomSlot = gameManager.allRows[(int)slotPosition.y - 1].mySlots[(int)slotPosition.x];
        }

        if((int)slotPosition.x - 1 >= 0)
        {
            leftSlot = gameManager.allRows[(int)slotPosition.y].mySlots[(int)slotPosition.x - 1];
        }

        if((int)slotPosition.x + 1 < gameManager.gridSize.x)
        {
            rightSlot = gameManager.allRows[(int)slotPosition.y].mySlots[(int)slotPosition.x + 1];
        }

        if(dotType)
        {
            float lerpValue = Mathf.Clamp01(Time.deltaTime * speed);
            Vector2 newPosition = Vector2.Lerp(dotType.gameObject.transform.position, transform.position, lerpValue);
            dotType.gameObject.transform.position = newPosition;
        }
        
        CheckCanDestory();
    }

    public void CheckCanDestory()
    {
        canDestory = false;

        if(dotType)
        {
            if(dotType.specialDot == DotType.Special.Normal)
            {
                if(topSlot)
                {
                    if(topSlot.dotType)
                    {
                        if(topSlot.dotType.dotColor == dotType.dotColor)
                        {
                            canDestory = true;
                        }
                    }
                }
                if(leftSlot)
                {
                    if(leftSlot.dotType)
                    {
                        if(leftSlot.dotType.dotColor == dotType.dotColor)
                        {
                            canDestory = true;
                        }
                    }
                }
                if(rightSlot)
                {
                    if(rightSlot.dotType)
                    {
                        if(rightSlot.dotType.dotColor == dotType.dotColor)
                        {
                            canDestory = true;
                        }
                    }
                }
                if(bottomSlot)
                {
                    if(bottomSlot.dotType)
                    {
                        if(bottomSlot.dotType.dotColor == dotType.dotColor)
                        {
                            canDestory = true;
                        }
                    }
                }
            }
        }
    }

    public void ChainDestory()
    {
        if(dotType.specialDot == DotType.Special.Normal)
        {
            if(canDestory)
            {
                dotType.DotDestory();
                gameManager.DotDestoryAtOnce ++;
            
                if(topSlot && topSlot.dotType)
                {
                    if(!topSlot.dotType.checkDestory)
                    {
                        if(topSlot.dotType.dotColor == dotType.dotColor)
                        {
                            topSlot.ChainDestory();
                        }
                    }
                }

                if(leftSlot && leftSlot.dotType)
                {
                    if(!leftSlot.dotType.checkDestory)
                    {
                        if(leftSlot.dotType.dotColor == dotType.dotColor)
                        {
                            leftSlot.ChainDestory();
                        }
                    }
                }

                if(rightSlot && rightSlot.dotType)
                {
                    if(!rightSlot.dotType.checkDestory)
                    {
                        if(rightSlot.dotType.dotColor == dotType.dotColor)
                        {
                            rightSlot.ChainDestory();
                        }
                    }
                }

                if(bottomSlot && bottomSlot.dotType)
                {
                    if(!bottomSlot.dotType.checkDestory)
                    {
                        if(bottomSlot.dotType.dotColor == dotType.dotColor)
                        {
                            bottomSlot.ChainDestory();
                        }
                    }
                }
            }
        } 
    }

    public void ForceDestory()
    {
        if(dotType.specialDot == DotType.Special.Normal)
        {
            dotType.DotDestory();
        }
        if(dotType.specialDot == DotType.Special.CrossBomb)
        {
            dotType.DotDestory();
        }
        else if(dotType.specialDot == DotType.Special.Disco)
        {
            dotType.DotDestory();
        }
    }
}
