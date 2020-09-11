using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    public Camera playerCam = null;
    //[SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    //[SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    //[SerializeField] private Transform playerTransform = null;
    //[SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    //private controls controls;
    //private controls controls
    //{
    //    get
    //    {
    //        if (controls != null) { return controls; }
    //        return controls = new controls();
    //    }
    //}

    //private CinemachineTransposer transposer;

    //public void Start()
    //{
    //    //Debug.Log("change camera");
    //    //virtualCamera.gameObject.SetActive(true);
    //   // Camera.main.gameObject.SetActive(false);
    //   // Camera.main.enabled = false;

    //   // playerCam.enabled = true;
    //   // playerCam.gameObject.SetActive(true);

    //}
    public override void OnStartAuthority()
    {
        //    // transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        //    Debug.Log("change camera");
        //    //virtualCamera.gameObject.SetActive(true);
        //    Camera.main.gameObject.SetActive(false);
        //    Camera.main.enabled = false;
         playerCam.enabled = true;
         playerCam.gameObject.SetActive(true);


        //    //enabled = true;

        //    //Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    //[ClientCallback]
    //private void OnEnable() => Controls.Enable();

    //[ClientCallback]
    //private void OnDisable() => Controls.Disable();

    //private void Look(Vector2 lookAxis)
    //{
    //    float deltaTime = Time.deltaTime;

    //    transposer.m_FollowOffset.y = Mathf.Clamp(
    //        transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
    //        maxFollowOffset.x,
    //        maxFollowOffset.y);

    //    playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    //}
}