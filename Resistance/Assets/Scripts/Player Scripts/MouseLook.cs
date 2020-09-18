using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Variables
    public Transform playerBody;
    public float xRot = 0f;
    [SerializeField] public float mouseSensitivity = 20f;

    void Start()
    {
        //lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        //hides the cursor
        Cursor.visible = false;
    }

    void Update() //calculated every frame
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 40f); //clamp mouse rotation

        //quaternions are responsible for rotations in unity
        transform.localRotation = Quaternion.Euler(xRot, 0, 0); 
        playerBody.Rotate(Vector3.up * mouseX);

    }
}