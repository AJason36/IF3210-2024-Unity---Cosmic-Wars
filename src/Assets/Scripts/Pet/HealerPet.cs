using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare
{
    public class HealerPet : MonoBehaviour
    {
        private GameObject player;
        public int healAmount = 2;
        private float healTimer = 2f;
        private float timeSinceLastHeal = 0f;

        // For Movement Behavior
        private Transform playerTransform;
        public NavMeshAgent nav;

        void Awake() 
        {
          player = GameObject.FindGameObjectWithTag("Player");
          playerTransform = player.transform;
        }
        void Update()
        {
            timeSinceLastHeal += Time.deltaTime;
            if (timeSinceLastHeal >= healTimer)
            {
                HealPlayer();
                timeSinceLastHeal = 0f;
            }
        }

        void HealPlayer()
        {
          // Set Navigation for Movement
          nav.SetDestination(playerTransform.position);

          // Heal
          PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
          if (playerHealth != null && playerHealth.currentHealth > 0)
          {
              playerHealth.Heal(healAmount);
          }
        }
    }
}