﻿using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Variables
    public Transform playerBody;
    public float xRot = 0f;
    public float mouseSensitivity = 15f;

    void Start()
    {
        //lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        //hides the cursor
        Cursor.visible = false;
    }

    void Update() //calculated every frame
    {
        Look();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 40f); //clamp mouse rotation

        //quaternions are responsible for rotations in unity
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    //Method for Unit Testing
    public float TestMouseLookX(float axis, float mouseSensitibity, float deltaTime)
    {
        float xRot = 0f;
        float mouseY = axis * mouseSensitibity * deltaTime;
        return xRot -= mouseY;
    }

    //Method for Unit Testing
    public float TestMouseLookY(float axis, float mouseSensitibity, float deltaTime)
    {
        float mouseX = axis * mouseSensitibity * deltaTime;
        Vector3 v3 = new Vector3(1, 1, 1);
        float playerBodyRotate = v3.y * mouseX;
        return playerBodyRotate;
    }
}