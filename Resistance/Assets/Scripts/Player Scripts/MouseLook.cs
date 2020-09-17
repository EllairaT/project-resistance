using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float xRot = 0f;
    [SerializeField] public float mouseSensitivity = 20f;

    void Start()
    {
        //lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() //calculated every frame
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 40f);

        //quaternions are responsible for rotations in unity
        transform.localRotation = Quaternion.Euler(xRot, 0, 0); 
        playerBody.Rotate(Vector3.up * mouseX);

    }
}