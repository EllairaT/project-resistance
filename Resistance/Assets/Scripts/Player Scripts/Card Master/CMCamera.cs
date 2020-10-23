using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMCamera : MonoBehaviour
{
    public float scrollSpeed = 30f;
    public float xRot = 0f;
    public float yRot = 90f;

    public float maxZoomIn = 5f;
    public float maxZoomOut = 60f;
    public float defaultFOV = 50f;
    private Camera playerCam;

    public float mouseSens = 15f;

    public bool canMove = true;

    void Start()
    {
        playerCam = GetComponent<Camera>();
        playerCam.fieldOfView = defaultFOV;
    }

    void Update()
    {
        if (canMove)
        {
            Look();

            float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

            if (scroll < 0 || Input.GetKey(KeyCode.Q))
            {
                if (playerCam.fieldOfView > maxZoomIn)
                {
                    ZoomIn();
                }
            }
            else if(scroll > 0 || Input.GetKey(KeyCode.E))
            {
                if (playerCam.fieldOfView < maxZoomOut)
                {
                    ZoomOut();
                }
            }
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, 0f, 60f); //look up and down

        yRot += mouseX;
        yRot = Mathf.Clamp(yRot, 45f, 140f); //left and right

        //rotate
        transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
    }

    void ZoomIn()
    {
        playerCam.fieldOfView -= Time.deltaTime * scrollSpeed;
    }

    void ZoomOut()
    {
        playerCam.fieldOfView += Time.deltaTime * scrollSpeed;
    }
}
