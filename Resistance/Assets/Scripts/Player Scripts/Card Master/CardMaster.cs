using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaster : MonoBehaviour
{
    public CMCamera CameraMove;

    private bool isCursorActive = true;

    private void Start()
    {
        ToggleCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursor();
        } 
    }

    void ToggleCursor()
    {
        if (isCursorActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        isCursorActive = !isCursorActive;
    }
}
