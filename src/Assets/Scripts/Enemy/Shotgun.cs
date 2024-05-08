using UnityEngine;

namespace Nightmare {
    public class Shotgun : MonoBehaviour {
        public int damage = 1; // Default damage
        public float speed = 10f; // Speed of the projectile
        public float maxDistance = 20f; // Maximum distance a pellet can travel

        private Vector3 startPosition;
        private Rigidbody rb;

        void Start() {
            startPosition = transform.position;
            rb = GetComponent<Rigidbody>();
            // Ensure that the Rigidbody component exists
            if (rb != null) {
                rb.velocity = transform.forward * speed;
            } else {
                Debug.LogError("Rigidbody component is missing from the projectile!", this);
            }
        }

        void Update() {
            if (rb != null && rb.velocity.sqrMagnitude == 0) {
                rb.velocity = transform.forward * speed;  
            }
            // Calculate the traveled distance more efficiently
            if ((transform.position - startPosition).sqrMagnitude > maxDistance * maxDistance) {
                Destroy(gameObject); // Destroy the projectile after it travels the maximum distance
            }
        }

        void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                // Ensure the PlayerHealth component exists
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null) {
                    playerHealth.TakeDamage(damage);
                    Destroy(gameObject); // Destroy the projectile on hitting the player
                } else {
                    Debug.LogError("PlayerHealth component missing on player object!", other);
                }
            }
        }
    }
}