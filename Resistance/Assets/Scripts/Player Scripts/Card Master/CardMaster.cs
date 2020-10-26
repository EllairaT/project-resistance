using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardMaster : NetworkBehaviour
{
    public CMCamera CameraMove;
    public Canvas cardMasterUI;
    public int gold = 1000;
    public GoldScript gs;

    private bool isCursorActive = true;

    public override void OnStartAuthority()
    {
        cardMasterUI.enabled = true;
        cardMasterUI.gameObject.SetActive(true);
    }

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
