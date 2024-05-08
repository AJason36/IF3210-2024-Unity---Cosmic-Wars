using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PlayerBullet : MonoBehaviour
    {
        public int damage = 40;
        public float speed = 10f;
        public float maxDistance = 20f;
        public bool isShotGun = false;

        private Vector3 startPosition;
        private Rigidbody rb;

        void Start()
        {
            startPosition = transform.position;
            rb = GetComponent<Rigidbody>();
            // Ensure that the Rigidbody component exists
            if (rb != null)
            {
                rb.velocity = transform.forward * speed;
            }
            else
            {
                Debug.LogError("Rigidbody component is missing from the projectile!", this);
            }
        }

        void Update()
        {
            if(!isShotGun){
                maxDistance = 50f;
            }
            if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
            {
                Destroy(gameObject); 
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Ensure the PlayerHealth component exists
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    if(isShotGun){
                        enemyHealth.TakeDamage(15);
                    }else{
                        enemyHealth.TakeDamage(damage);
                    }
                    Destroy(gameObject); 
                }
                else
                {
                    Debug.LogError("EnemyHealth component missing on player object!", other);
                }
            }
        }
    }

}