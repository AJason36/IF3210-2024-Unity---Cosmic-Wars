using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare{
    public class PoisonArea : MonoBehaviour
    {
        public int damagePerSecond = 2; 
        public float timeBetweenAttacks = 1.0f;
        public bool isBoss = false;
        public int speedReduction = 1;

        private GameObject player;
        private PlayerHealth playerHealth;
        private EnemyHealth enemyHealth;
        private PlayerMovement playerMovement;
        private Animator anim;
        private bool playerInRange = false;
        private float timer = 0.0f;

        void Awake()
        {
            // Assume the player and its components are correctly tagged and available
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMovement = player.GetComponent<PlayerMovement>();
            anim = GetComponentInParent<Animator>();
            enemyHealth = GetComponentInParent<EnemyHealth>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
                RestorePlayerSpeed();
                timer = 0.0f;
            }
        }

        void Update()
        {
            if (playerInRange && !enemyHealth.IsDead())
            {
                timer += Time.deltaTime;

                if (timer >= timeBetweenAttacks)
                {
                    Poison();
                    if(isBoss) {
                        ReducePlayerSpeed();
                    }
                }
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Poison()
        {
            if (playerHealth != null && playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(damagePerSecond);
                timer = 0.0f;
            }
        }

        private void ReducePlayerSpeed() {
            if (playerMovement != null && playerMovement.speed > 1) {
                playerMovement.speed -= speedReduction; 
            }
        }

        private void RestorePlayerSpeed() {
            if (playerMovement != null) {
                playerMovement.speed = playerMovement.initialSpeed; 
            }
        }
    }
}