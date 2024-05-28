using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject rowPrefab;
    public GameObject specialCrossBombPrefab;
    public GameObject specialDiscoPrefab;
    public GameObject slotPrefab;
    
    public List<AllRow> allRows = new List<AllRow>();

    bool _starting;
    bool isStart;
    bool checkFillSlot;
    public TextMeshProUGUI textTimer;
    public GameObject textTimerPanel;
    public float startTime;
    float startTimer;
    
    public Transform gridContainer;
    public Vector2 gridSize;
    float gridScale;

    public int DotDestoryAtOnce;
    bool dotSpecial;
    public Vector2 lastSlot;
    public bool checkSpawnSpecial;
    float delayCount = 0.15f;

    [System.Serializable]
    public class AllRow
    {
        public MyRow myRow;
        public List<MySlot> mySlots = new List<MySlot>();
    }

    private void Start() 
    {
        startTimer = startTime;
        GenerateGrid();
    }
    public void GetAllRowAndSlot(int x, int y)
    {
        gridSize = new Vector2(x,y);
        foreach(MyRow myRow in gridContainer.GetComponentsInChildren<MyRow>())
        {
            AllRow allRow = new AllRow();
            allRow.myRow = myRow;

            allRows.Add(allRow);
        }

        for(int i = 0; i < x; i++)
        {
            foreach(MySlot mySlot in allRows[i].myRow.gameObject.GetComponentsInChildren<MySlot>())
            {
                allRows[i].mySlots.Add(mySlot);
            }
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _starting = true;
        }

        if(_starting)
        {
            textTimerPanel.SetActive(true);
            textTimer.text = startTimer.ToString("N0");
            startTimer -= Time.deltaTime;

            if(startTimer < 0)
            {
                SpawnDotToAllSlots();
                startTimer = 0;
                isStart = true;
                _starting = false;
                textTimerPanel.SetActive(false);
            }
        }

        if(DotDestoryAtOnce != 0 || dotSpecial)
        {
            delayCount -= Time.deltaTime;
            if(delayCount < 0)
            {
                DotDestoryAtOnce = 0;
                delayCount = 0.15f;
                dotSpecial = false;

                CheckDotDrop();
                checkFillSlot = false;
            }

            if(!checkSpawnSpecial)
            {
                if(DotDestoryAtOnce >= 6 && DotDestoryAtOnce < 10)
                {
                    GameObject dotSpecial = Instantiate(specialCrossBombPrefab,allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x].transform.position,Quaternion.identity);
                    dotSpecial.GetComponent<DotType>().currentSlot = allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x];
                    allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x].dotType = dotSpecial.GetComponent<DotType>();
                }
                else if(DotDestoryAtOnce >= 10)
                {
                    GameObject dotSpecial = Instantiate(specialDiscoPrefab,allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x].transform.position,Quaternion.identity);
                    dotSpecial.GetComponent<DotType>().currentSlot = allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x];
                    allRows[(int)lastSlot.y].mySlots[(int)lastSlot.x].dotType = dotSpecial.GetComponent<DotType>();
                }
                checkSpawnSpecial = true;
            }
        }
        else
        {
            checkSpawnSpecial = false;

            if(isStart)
            {
                if(!checkFillSlot)
                {
                    FillEmptySlot();
                    checkFillSlot = true;
                }
            }
        }
    }

    public void SpawnDotToAllSlots()
    {
        for(int i = 0;i < gridSize.y;i++)
        {
            for(int a = 0;a < gridSize.x;a++)
            {
                GameObject _dot = Instantiate(dotPrefab,allRows[i].mySlots[a].transform.position,Quaternion.identity);
                allRows[i].mySlots[a].GetComponent<MySlot>().dotType = _dot.GetComponent<DotType>();
                _dot.GetComponent<DotType>().currentSlot = allRows[i].mySlots[a];
                _dot.transform.localScale = new Vector2(gridScale - 0.05f,gridScale - 0.05f);
            }
        }
    }

    public void GenerateGrid()
    {
        Vector3 scale = slotPrefab.transform.localScale;
        gridScale = scale.x;

        for(int i = 0; i < gridSize.y; i++)
        {
            GameObject row = Instantiate(rowPrefab,Vector2.zero,Quaternion.identity);
            row.GetComponent<MyRow>().rowNumber = i;
            
            for(int a = 0; a < gridSize.x; a++)
            {
                GameObject slot = Instantiate(slotPrefab,row.transform.position,Quaternion.identity);
                slot.GetComponent<MySlot>().slotPosition = new Vector2(a,i);
                slot.transform.position = new Vector2(a * gridScale, 0);
                slot.transform.SetParent(row.transform);
                slot.name = "Slot " + a.ToString();
            }

            row.transform.position = new Vector2(0, i * gridScale);
            row.transform.SetParent(gridContainer);
            row.name = "Row " + i.ToString();
        }
        gridContainer.transform.position = new Vector2(-(gridSize.x - 1)  * (gridScale * 0.5f), -(gridSize.y - 1) * (gridScale * 0.5f));

        GetAllRowAndSlot((int)gridSize.x,(int)gridSize.y);
    }

    public void CheckDotDrop()
    {
        for(int i = 0; i < gridSize.y; i++)
        {
            for(int a = 0; a < gridSize.x; a++)
            {
                if(allRows[i].mySlots[a])
                {
                    if(!allRows[i].mySlots[a].dotType)
                    {
                        DropDots(a,i);
                    }  
                }
            }
        }
    }

    public void DropDots(int vectorX,int vectorY)
    {
        for(int i = vectorY; i < gridSize.y; i++)
        {
            if(allRows[i].mySlots[vectorX].dotType)
            {
                allRows[vectorY].mySlots[vectorX].dotType = allRows[i].mySlots[vectorX].dotType;
                allRows[i].mySlots[vectorX].dotType = null;
                //allRows[vectorY].mySlots[vectorX].dotType.transform.position = allRows[vectorY].mySlots[vectorX].transform.position;
                break;
            }  
        }
    }

    public void DotsDestory(int vectorX, int vectorY)
    {
        if(allRows[vectorY].mySlots[vectorX].dotType.specialDot == DotType.Special.Normal)
        {
            allRows[vectorY].mySlots[vectorX].ChainDestory();
        }
        else if(allRows[vectorY].mySlots[vectorX].dotType.specialDot == DotType.Special.CrossBomb)
        {
            crossBombDestory(vectorX,vectorY);
            dotSpecial = true;
        }
        else if(allRows[vectorY].mySlots[vectorX].dotType.specialDot == DotType.Special.Disco)
        {
            allRows[vectorY].mySlots[vectorX].ForceDestory();

            int number = Random.Range(0,4);
            switch (number)
            {
                case 0:
                    DiscoDestory(DotType.DotColor.Red);
                    break;

                case 1:
                     DiscoDestory(DotType.DotColor.Green);
                    break;

                case 2:
                     DiscoDestory(DotType.DotColor.Blue);
                    break;

                case 3:
                     DiscoDestory(DotType.DotColor.Yellow);
                    break;
            }

            dotSpecial = true;
        }
        lastSlot = new Vector2(vectorX,vectorY);
    }

    public void crossBombDestory(int vectorX, int vectorY)
    {
        for(int i = 0; i < gridSize.y; i++)
            {
                if(allRows[i].mySlots[vectorX].dotType)
                {
                    allRows[i].mySlots[vectorX].ForceDestory();
                }
            }

            for(int a = 0; a < gridSize.x; a++)
            {
                if(allRows[vectorY].mySlots[a].dotType)
                {
                    allRows[vectorY].mySlots[a].ForceDestory();
                }
            }
    }

    public void DiscoDestory(DotType.DotColor dotColor)
    {
        for(int i = 0; i < gridSize.y; i++)
        {
            for(int a = 0; a < gridSize.x; a++)
            {
                if(allRows[i].mySlots[a])
                {
                    if(allRows[i].mySlots[a].dotType)
                    {
                        if(allRows[i].mySlots[a].dotType.dotColor == dotColor)
                        {
                            allRows[i].mySlots[a].ForceDestory();
                        }
                    }  
                }
            }
        }
    }

    public void FillEmptySlot()
    {
         for(int i = 0; i < gridSize.y; i++)
        {
            for(int a = 0; a < gridSize.x; a++)
            {
                if(allRows[i].mySlots[a])
                {
                    if(!allRows[i].mySlots[a].dotType)
                    {
                        GameObject _dot = Instantiate(dotPrefab,new Vector2(allRows[i].mySlots[a].transform.position.x,allRows[i].mySlots[a].transform.position.y + 5),Quaternion.identity);

                        allRows[i].mySlots[a].GetComponent<MySlot>().dotType = _dot.GetComponent<DotType>();
                        _dot.GetComponent<DotType>().currentSlot = allRows[i].mySlots[a];
                        _dot.transform.localScale = new Vector2(gridScale - 0.05f,gridScale - 0.05f);
                    }  
                }
            }
        }
    }
}
