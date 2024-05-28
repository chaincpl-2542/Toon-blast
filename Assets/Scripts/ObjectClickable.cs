using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectClickable : MonoBehaviour
{
    private Camera _mainCamera;
    MySlot mySlot;
    public GameManager gameManager;
    private void Awake() 
    {
        _mainCamera = Camera.main;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        mySlot = rayHit.collider.gameObject.GetComponent<MySlot>();

        if(mySlot.dotType)
        {
            gameManager.DotsDestory((int)mySlot.slotPosition.x,(int)mySlot.slotPosition.y);
        }
    }
}
