using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PetHealth : MonoBehaviour
{

    GameObject infoText;
    public int startingHealth = 50;
    int currentHealth;

    public bool godMode = false;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
      infoText = GameObject.FindGameObjectWithTag("Info");
      currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
          Destroy(gameObject);
        }
    }

    public int getCurrentHealth(){
      return currentHealth;
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
      InfoManager infoManager = infoText.GetComponent<InfoManager>();
      infoManager.SetInfo("The Pet has Died!");
    }

      public void setGodMode(bool newSet){
        godMode = newSet;
      }

      public bool getGodMode(){
        return godMode;
      }
}
