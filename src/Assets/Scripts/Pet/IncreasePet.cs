using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare{
    public class IncreasePet : MonoBehaviour
    {
        public GameObject boss;
        public float buffPercentage = 0.2f;  // 20%
        EnemyAttack attackComponent;
        LongRangeAttack longRangeAttackComponent;
        
        void Awake ()
        {
            // Setting up the references.
            attackComponent = boss.GetComponent<EnemyAttack>();
            longRangeAttackComponent = boss.GetComponent<LongRangeAttack>();
        }
        void Start()
        {            
            IncreaseAttack(buffPercentage);
        }

        void Update() {
            if (boss == null) {  // Check if boss has been destroyed
                Destroy(gameObject);  // Destroy the pet
            }
        }

        void IncreaseAttack(float percentage)
        {
            if (attackComponent != null)
            {
                attackComponent.buffPercentage += percentage;
            }
            if (longRangeAttackComponent != null)
            {
                longRangeAttackComponent.buffPercentage += percentage;
            }
        }

        void OnDestroy() {
            if(boss)DecreaseAttack(buffPercentage);
        }

        void DecreaseAttack(float percentage) {
            if (attackComponent != null) {
                attackComponent.buffPercentage -= percentage;
            }

            if (longRangeAttackComponent != null) {
                longRangeAttackComponent.buffPercentage -= percentage;
            }
        }
    }
}
