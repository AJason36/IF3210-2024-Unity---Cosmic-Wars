using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
   public class LongRangeAttack : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform shootingPoint;
        public int pelletsCount = 10;
        public float spreadAngle = 15f; 
        public float timeBetweenAttacks = 1.5f;
        public int attackDamage = 10;

        private Animator anim;
        private GameObject player;
        private PlayerHealth playerHealth;
        private EnemyHealth enemyHealth;
        private EnemyMovement enemyMovement; 
        public ParticleSystem plasmaExplosionEffect;

        private bool playerInRange;
        private float timer;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            enemyMovement = GetComponent<EnemyMovement>();  // Get the enemy movement script
            anim = GetComponent<Animator>();
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
            }
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && playerInRange)
            {
                enemyMovement.StopMovement();
                Attack ();
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack() {
            timer = 0f;
            if (projectilePrefab == null) {
                Debug.LogError("Projectile prefab is not assigned or was destroyed.");
                return;
            }

            if (plasmaExplosionEffect != null) {
                plasmaExplosionEffect.transform.position = shootingPoint.position;
                plasmaExplosionEffect.Play();
                // StartCoroutine(StopParticleSystem(plasmaExplosionEffect, plasmaExplosionEffect.main.duration));
            } else {
                Debug.LogError("Plasma Explosion Effect is not assigned!");
            }
            for (int i = 0; i < pelletsCount; i++) {
                float spread = Random.Range(-spreadAngle, spreadAngle);
                Vector3 shootDirection = Quaternion.Euler(0, spread, 0) * transform.forward;
                GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(shootDirection));
                if (projectile != null) {
                    Shotgun shotgunScript = projectile.GetComponent<Shotgun>();
                    if (shotgunScript != null) {
                        shotgunScript.damage = attackDamage / pelletsCount;
                    } else {
                        Debug.LogError("Shotgun component not found on the instantiated projectile!", projectile);
                    }
                } else {
                    Debug.LogError("Failed to instantiate projectile", this);
                }
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
