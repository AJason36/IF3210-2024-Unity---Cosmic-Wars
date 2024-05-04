using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
 
    public float speed = 12f;
    public float gravity = -9.81f * 4;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator animator;
 
    Vector3 velocity;
    bool isGrounded;
    bool isRunning;
    float actualSpeed;
 
    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Jumping", false);
        }
 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;

        // Check if running
        isRunning = Input.GetButton("Run");
        if (isRunning) {
            actualSpeed = speed;
            animator.SetBool("Running", true);
        } else {
            actualSpeed = speed/2;
            animator.SetBool("Running", false);
        }


        controller.Move(move * actualSpeed * Time.deltaTime);
        
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jumping", true);
        }

        if (move.sqrMagnitude > 0.01f){
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
}
}