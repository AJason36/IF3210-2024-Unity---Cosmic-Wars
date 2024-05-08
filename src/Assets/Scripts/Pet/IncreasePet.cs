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
            DecreaseAttack(boss, buffPercentage);
        }

        void DecreaseAttack(GameObject character, float percentage) {
            if (attackComponent != null) {
                attackComponent.buffPercentage -= percentage;
            }

            if (longRangeAttackComponent != null) {
                longRangeAttackComponent.buffPercentage -= percentage;
            }
        }
    }
}
