using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetHealth : MonoBehaviour
{

    public int startingHealth = 50;
    int currentHealth;

    public bool godMode = false;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
          Destroy(gameObject);
        }
    }

    public void TakeDamage (int amount) {
      if (godMode){
        return;
      }

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
