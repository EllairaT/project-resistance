using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    Vector3 playerVelocity;

    public Transform GroundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && playerVelocity.y < 0)
        {
            //this condition might be true before model is completely on the ground
            //so set this to -2 to force the model on the ground. 
            playerVelocity.y = -4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (horizontalSpeed > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetTrigger("Jump");
        }
        else if(Input.GetButton("Crouching")&& isGrounded)
        {
            anim.SetBool("isCrouching", true);
            anim.SetTrigger("Crouch");
        }
    }
}
