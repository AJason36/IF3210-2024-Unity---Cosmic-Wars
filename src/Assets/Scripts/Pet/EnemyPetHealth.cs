using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPetHealth : MonoBehaviour
{

    public int startingHealth = 50;
    int currentHealth;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (int amount) {
      currentHealth -= amount;

      if (currentHealth <= 0 && !isDead){
        Death();
      }
    }

    void Death(){
      isDead = true;
      // Add condition pet dies
    }
}
