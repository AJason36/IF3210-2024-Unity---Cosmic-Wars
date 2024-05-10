using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;
        public int initialDamage;
        public float buffPercentage = 1;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        private EnemyMovement enemyMovement; 
        bool playerInRange;
        float timer;

         GameObject pet;
         PetHealth petHealth;
         bool petInRange;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            if(!player) { Debug.Log("Player not found!"); } 
            else { Debug.Log("Player found!"); }

            playerHealth = player.GetComponent <PlayerHealth> ();
            if(!playerHealth)
            {
                Debug.Log("Player health not found!");
            }
            enemyMovement = GetComponent<EnemyMovement>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
            initialDamage = attackDamage;
            // StartPausible();

            // For Pet
            pet = GameObject.FindGameObjectWithTag ("Pet");
            if(!pet) { Debug.Log("Pet not found!"); } 
            else { Debug.Log("Pet found!"); }

            petHealth = pet.GetComponent <PetHealth> ();
            if(!petHealth)
            {
                Debug.Log("Pet health not found!");
            }
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

            if (other.gameObject == pet){
                petInRange = true;
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
            if (other.gameObject == pet){
                petInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused)
                return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth() > 0 )
            {
                enemyMovement.StopMovement();
                Attack ();
            }

            // If the player has zero or less health...
            if(playerHealth.getIsDead())
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }

            // Check for pet
            if(timer >= timeBetweenAttacks && petInRange && enemyHealth.CurrentHealth() > 0 )
            {
                enemyMovement.StopMovement();
                AttackPet();
            }


        }

        void Attack ()
        {   
            // Reset the timer.
            timer = 0f;
            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage ((int)(attackDamage*buffPercentage));
            }
            StartCoroutine(ResumeMovementAfterDelay(timeBetweenAttacks));  // Resume movement after attack delay
        }

        void AttackPet ()
        {   
            // Reset the timer.
            timer = 0f;
            // If the player has health to lose...
            if(petHealth.getCurrentHealth() > 0)
            {
                // ... damage the player.
                petHealth.TakeDamage ((int)(attackDamage*buffPercentage));
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