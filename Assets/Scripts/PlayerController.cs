using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Movement
    public PlayerAction inputAction;
    Vector2 move;
    Rigidbody rb;
    private float walkSpeed = 5f;

    public Camera playerCam;

    //Jump
    private bool isGrounded = true;
    public float jump = 5f;

    //Animation
    Animator playerAnimator;
    private bool isWalking = false;

    //Shooting
    public GameObject bullet;
    public Transform bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        //Create an instance of PlayerController if one doesn't exist
        if (!instance)
        {
            instance = this;
        }

        //Using controller from PlayerInputController
        inputAction = PlayerInputController.controller.inputAction;

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        //Add contexts for move listener
        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        //Add context for jump listener
        inputAction.Player.Jump.performed += cntxt => Jump();

        //Add context for shoot listener
        inputAction.Player.Shoot.performed += cntxt => Shoot();

        //Freeze the rotation of the rigid body, ensuring it doesn't fall over
        rb.freezeRotation = true;
    }

    private void Jump()
    {
        //If the player is grounded, add an upwards component to the velocity
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    private void Shoot()
    {
        //Clone the base bullet and place it in front of the barrel of the gun
        Rigidbody bulletRb = Instantiate(bullet, bulletPos.position, Quaternion.identity).GetComponent<Rigidbody>();
       
        //Propel the bullet forward and upward
        bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //Set the rotation of the player around the y-axis equal to the camera's roation around the y (rotates player horizontally)
        transform.rotation = Quaternion.Euler(new Vector3(0f, playerCam.transform.rotation.eulerAngles.y, 0f));

        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        //To find if the player is on the ground, raycats downwards from the player's transform position.
        //Since the transform position is at the bottom of the model, use a very small number for the length of the raycast
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);

        //If the player is not moving horizontally, use idle animation
        if (move == Vector2.zero) isWalking = false;

        //Else, use walking animation
        else isWalking = true;

        //Set the animation parameter to the current walking state
        playerAnimator.SetBool("IsWalking", isWalking);
    }
}
