using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PlayerBullet : MonoBehaviour
    {
        private StatisticsManager statisticsManager;
        public int damage = 40;
        public float speed = 10f;
        public float maxDistance = 30f;
        public bool isShotGun = false;

        private Vector3 startPosition;
        private Rigidbody rb;

        void Start()
        {
            statisticsManager = StatisticsManager.Instance;
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
            if(other is CapsuleCollider ){
            if (other.gameObject.CompareTag("Jendral") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyPet"))
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth.currentHealth>0)
                {
                    statisticsManager.RecordSuccessfulShot();
                    if(isShotGun){
                        int pelletDmg = damage * 2 / 5;
                        enemyHealth.TakeDamage(pelletDmg);
                            Debug.Log(pelletDmg);
                    }else{
                        enemyHealth.TakeDamage(damage);
                    }
                    Destroy(gameObject); 
                }
            }}
        }
    }

}