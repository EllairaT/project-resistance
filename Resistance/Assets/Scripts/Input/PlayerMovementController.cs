using Mirror;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    private Vector3 playerVelocity;
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

    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    public override void OnStartAuthority()
    {
        enabled = true;
        anim = GetComponent<Animator>();

        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void Update() => Move();

    [Client]
    private void SetMovement(Vector2 movement)
    {
        previousInput = movement;
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
    }
    //private void SetMovement(Vector2 movement) => previousInput = movement;

    [Client]
    private void ResetMovement()
    {
        previousInput = Vector2.zero;
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
    }
    //private void ResetMovement() => previousInput = Vector2.zero;

    [Client]
    private void Move()
    {
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
    }
}

//float x = Input.GetAxis("Horizontal");
// float z = Input.GetAxis("Vertical");

// Vector3 move = controller.transform.right.normalized * previousInput.x + controller.transform.forward.normalized * previousInput.y;
//controller.Move(move * movementSpeed * Time.deltaTime);

//playerVelocity.y += gravity* Time.deltaTime;
//controller.Move(playerVelocity* Time.deltaTime);

        //Vector3 right = controller.transform.right;
        // Vector3 forward = controller.transform.forward;
        //right.y = 0f;
        //forward.y = 0f;

       // Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        //controller.Move(movement * movementSpeed * Time.deltaTime);