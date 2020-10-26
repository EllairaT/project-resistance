using Mirror;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    //Variables
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private NetworkAnimator networkAnim;

    public float gravity = -19f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundDistance = 5f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    private float jumpHeight = 5f; //needs to be implemented
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

    //When instantiated...
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
    private void SetMovement(Vector2 movement) //player is moving, set input and set animation to moving
    {
        previousInput = movement;
        networkAnim.ResetTrigger("isIdle");
        networkAnim.SetTrigger("isWalking");   
    }

    [Client]
    private void ResetMovement() //player has stopped moving, set input to 0 and set animation to idle
    {
        previousInput = Vector2.zero;
        networkAnim.ResetTrigger("isWalking");
        networkAnim.SetTrigger("isIdle");
    }

    [Client]
    private void Move() //Method used to move the actual player and ensure the stay on the ground (gravity)
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

    [Client]
    private void Jump()
    {
        networkAnim.SetTrigger("Jump");
        transform.position = new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z);
    }
    //Method for Unit Testing
    public Vector3 CalculateMovement(float xAxis, float zAxis, float deltaTime)
    {
        var x = xAxis * movementSpeed * deltaTime;
        var z = zAxis * movementSpeed * deltaTime;

        return new Vector3(0, 0, 0);
    }
}