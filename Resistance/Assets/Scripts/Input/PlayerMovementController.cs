using Mirror;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private NetworkAnimator networkAnim;

    //-----------------------
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    private float jumpHeight = 1f;
    private Vector3 playerVelocity;
    //---------------------------------

    private Vector2 previousInput;


    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        networkAnim = GetComponent<NetworkAnimator>();
        
        enabled = true;
        
        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    [ClientCallback]
    private void Update() => Move();

    [Client]
    //private void SetMovement(Vector2 movement) => previousInput = movement;
    private void SetMovement(Vector2 movement)
    {
        previousInput = movement;
        networkAnim.ResetTrigger("isIdle");
        networkAnim.SetTrigger("isWalking");
        
        //networkAnim.animator.SetBool("isWalking", true);
        //networkAnim.animator.SetBool("isIdle", false);    
    }

    [Client]
    //private void ResetMovement() => previousInput = Vector2.zero;
    private void ResetMovement()
    {
        previousInput = Vector2.zero;
        //networkAnim.animator.SetBool("isWalking", false);
        //networkAnim.animator.SetBool("isIdle", true);
        networkAnim.ResetTrigger("isWalking");
        networkAnim.SetTrigger("isIdle");
    }

    [Client]
    private void Move()
    {
        //---------------------------------------
        //Vector3 right = controller.transform.right;
        //Vector3 forward = controller.transform.forward;
        //right.y = 0f;
        //forward.y = 0f;

        //Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        //controller.Move(movement * movementSpeed * Time.deltaTime);
        //--------------------------------------------

        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && playerVelocity.y < 0)
        {
            //this condition might be true before model is completely on the ground
            //so set this to -2 to force the model on the ground. 
            playerVelocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * movementSpeed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Vector3 move = transform.right * x + transform.forward * z;
        // Vector3 move = controller.transform.right.normalized * previousInput.x + controller.transform.forward.normalized * previousInput.y;
        //  controller.Move(move * movementSpeed * Time.deltaTime);

        // playerVelocity.y += gravity * Time.deltaTime;
        //  controller.Move(playerVelocity * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //    anim.SetTrigger("Jump");
        //}
    }
}