using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class HealerPet : MonoBehaviour
    {
        private GameObject player;
        public int healAmount = 2;
        private float healTimer = 2f;
        private float timeSinceLastHeal = 0f;

        void Awake() 
        {
          player = GameObject.FindGameObjectWithTag("Player");
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
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.currentHealth > 0)
            {
                playerHealth.Heal(healAmount);
            }
        }
    }
}