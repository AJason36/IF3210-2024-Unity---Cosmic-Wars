using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Nightmare
{
    public class AttackerPet : MonoBehaviour
    {
        public int attackDamage = 100;
        public float timeBetweenAttacks = 1f;
        public float moveSpeed = 5f;

        public float distanceThreshold = 10f;
        public NavMeshAgent nav;
        private GameObject nearestEnemy;
        private EnemyHealth enemyHealth;

        bool enemyInRange;
        float timer;

        // For Movement Behavior
        private GameObject player;
        private Transform playerTransform;

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            nearestEnemy = null;
        }
        void Update()
        {
            FindNearestEnemy();
            timer += Time.deltaTime;
            if(player==null){
                Destroy(gameObject);
            }
            if (nearestEnemy != null)
            {
              if (timer >= timeBetweenAttacks){
                AttackNearestEnemy();
              }
            }
            // Case there is no near enemy, get to player
            else if (player){
              nav.SetDestination(playerTransform.position);
            }
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == nearestEnemy)
            {
                // ... the enemy is in range.
                enemyInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the enemy...
            if(other.gameObject == nearestEnemy)
            {
                // ... the player is no longer in range.
                enemyInRange = false;
            }
        }

        void FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    enemyHealth = enemy.GetComponent<EnemyHealth>();

                    if (enemyHealth.currentHealth > 0)
                    {
                        closestEnemy = enemy;
                    }
                }
            }

            nearestEnemy = closestEnemy;
        }

        void AttackNearestEnemy()
        {
            nav.SetDestination(nearestEnemy.transform.position);
            timer = 0f;
            enemyHealth = nearestEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth.currentHealth>0 && enemyInRange)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
}