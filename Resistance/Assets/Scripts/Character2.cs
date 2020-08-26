using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character2 : MonoBehaviour
{

    static Animator anim;
    public float speed = 5.0f;
    public float rotationSpeed = 200.0f;

    private Rigidbody rb;
    public Vector3 jump;
    public float jumpForce = 10f;
    public BoxCollider col;
    public bool isGrounded;

    #region Monobehaviour API

    void Start()
    {
        anim = GetComponent<Animator>(); //call animator function of character
        rb = GetComponent<Rigidbody>(); //call rigidbody component
        col = GetComponent<BoxCollider>();
        jump = new Vector3(0.0f, 100.0f, 0.0f);
    }


    void OnCollisionStay()
    {
        isGrounded = true;
        Debug.Log("collission stay");
    }


    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation); //movement

        transform.Rotate(0, rotation, 0);

        Vector3 movement = new Vector3(rotation, 0, translation);
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

        if (translation != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }



        Debug.Log("walking");
    }

    void OnCollissionExit()
    {
        Debug.Log("not colliding");
        isGrounded = false;
        anim.SetBool("isWalking", false);
    }
    #endregion
}
