using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaster : MonoBehaviour
{

    public GameObject CardMenu;
    public CMCamera CameraMove;

    private bool isMenuActive = false;
    private bool isCursorActive = true;

    private void Start()
    {
        CardMenu.SetActive(false);
        ToggleCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursor();
        } 
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleMenu();
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        isCursorActive = !isCursorActive;
    }

    void ToggleMenu()
    {
        if (isMenuActive)
        {
            CardMenu.SetActive(false);
            CameraMove.canMove = true;
        }
        else
        {
            CardMenu.SetActive(true);
            CameraMove.canMove = false;
            isCursorActive = false;
            ToggleCursor();
        }

        isMenuActive = !isMenuActive;
    }

}
