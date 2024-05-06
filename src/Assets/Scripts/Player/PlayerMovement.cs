using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;

namespace Nightmare
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        PlayerHealth playerHealth;

        public float speed = 12f;
        public float initialSpeed;
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

        void Awake()
        {
            playerHealth = GetComponent<PlayerHealth>();
            initialSpeed = speed; // Store the initial speed
        }

        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                animator.SetBool("Jumping", false);
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            isRunning = Input.GetButton("Run");
            actualSpeed = isRunning ? speed : speed / 2;
            animator.SetBool("Running", isRunning);
            animator.SetBool("Walking", move.sqrMagnitude > 0.01f);

            controller.Move(move * actualSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                animator.SetBool("Jumping", true);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered trigger with: " + other.gameObject.name);
            if (other.CompareTag("HealthOrb"))
            {
                playerHealth.DrinkHealthOrb();
                Destroy(other.gameObject); // Destroy the orb after use
            }
            else if (other.CompareTag("SpeedOrb"))
            {
                Debug.Log("Masuk ke attack");
                StartCoroutine(TemporarySpeedBoost()); // Start coroutine for temporary speed boost
                Destroy(other.gameObject); // Destroy the orb after use
            }
        }

        IEnumerator TemporarySpeedBoost()
        {
            speed += 0.2f * initialSpeed;
            yield return new WaitForSeconds(5f);
            speed = initialSpeed;
        }
    }
}
