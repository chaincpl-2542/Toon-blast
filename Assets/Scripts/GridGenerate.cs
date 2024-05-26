using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using UnityEngine;

public class GridGenerate : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject slotPrefab;
    public Transform gridContainer;
    public Vector2 gridSize;
    GameManager gameManager;
    float gridScale;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();

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

        gameManager.GetAllRowAndSlot((int)gridSize.x,(int)gridSize.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
