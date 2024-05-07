using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        private EnemyMovement enemyMovement; 
        bool playerInRange;
        float timer;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyMovement = GetComponent<EnemyMovement>();
            // enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

            // StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused)
                return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange /* && enemyHealth.CurrentHealth() > 0 */)
            {
                enemyMovement.StopMovement();
                Attack ();
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void Attack ()
        {   
            
            // Reset the timer.
            timer = 0f;
            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                Debug.Log("Damage the player");
                // ... damage the player.
                playerHealth.TakeDamage (attackDamage);
            }
            StartCoroutine(ResumeMovementAfterDelay(timeBetweenAttacks));  // Resume movement after attack delay

        }
        IEnumerator ResumeMovementAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            enemyMovement.StartMovement();  // Re-enable movement
        }
    }
}