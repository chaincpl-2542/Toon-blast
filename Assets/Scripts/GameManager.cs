using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject rowPrefab;
    public GameObject slotPrefab;
    public List<AllRow> allRows = new List<AllRow>();

    bool _starting;
    public float startTime;
    float startTimer;
    
    public Transform gridContainer;
    public Vector2 gridSize;
    float gridScale;

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
        if(Input.GetKeyDown(KeyCode.P))
        {
            _starting = true;
        }

        if(_starting)
        {
           
            startTimer -= Time.deltaTime;

            if(startTimer < 0)
            {
                SpawnDotToAllSlots();
                startTimer = 0;
                _starting = false;
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
            Transform row = Instantiate(rowPrefab,Vector2.zero,Quaternion.identity).transform;
            
            for(int a = 0; a < gridSize.x; a++)
            {
                Transform slot = Instantiate(slotPrefab,row.transform.position,Quaternion.identity).transform;
                slot.transform.position = new Vector2(a * gridScale, 0);
                slot.transform.SetParent(row);
                slot.name = "Slot " + a.ToString();
            }

            row.transform.position = new Vector2(0, i * gridScale);
            row.SetParent(gridContainer);
            row.name = "Row " + i.ToString();
        }
        gridContainer.transform.position = new Vector2(-(gridSize.x - 1)  * (gridScale * 0.5f), -(gridSize.y - 1) * (gridScale * 0.5f));

        GetAllRowAndSlot((int)gridSize.x,(int)gridSize.y);
    }
}
