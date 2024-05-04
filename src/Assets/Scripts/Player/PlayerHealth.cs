using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        public bool godMode = false;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovement playerMovement;
        PlayerShooting playerShooting;
        bool isDead;
        bool damaged;

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            // playerShooting = GetComponentInChildren<PlayerShooting>();
            currentHealth = startingHealth;

            // ResetPlayer();
        }

        public void ResetPlayer()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;

            playerMovement.enabled = true;
            playerShooting.enabled = true;

            anim.SetBool("IsDead", false);
        }


        void Update()
        {
            // If the player has just been damaged...
            if (damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }


        public void TakeDamage(int amount)
        {
            if (godMode)
                return;

            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            // Play the hurt sound effect.
            playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }
        void Death()
        {
            isDead = true;

            // Animations
            if (anim != null)
            {
                anim.SetTrigger("Die");
            }
            else
            {
                Debug.LogError("Animator component missing on " + gameObject.name);
            }

            // Sound
            if (playerAudio != null)
            {
                playerAudio.clip = deathClip;
                playerAudio.Play();
            }
            else
            {
                Debug.LogError("AudioSource component missing on " + gameObject.name);
            }

            // Movement and Shooting Scripts
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            else
            {
                Debug.LogError("PlayerMovement component missing on " + gameObject.name);
            }

            if (playerShooting != null)
            {
                // playerShooting.DisableEffects();  // Assuming this is your intention.
                playerShooting.enabled = false;
            }
            else
            {
                Debug.LogError("PlayerShooting component missing or not properly configured on " + gameObject.name);
            }
        }

        // void Death()
        // {
        //     // Set the death flag so this function won't be called again.
        //     isDead = true;

        //     // Turn off any remaining shooting effects.
        //     // playerShooting.DisableEffects();

        //     // Tell the animator that the player is dead.
        //     // anim.SetBool("IsDead", true);
        //     anim.SetTrigger("Die");
        //     // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            
        //     playerAudio.clip = deathClip;
        //     playerAudio.Play();

        //     // Turn off the movement and shooting scripts.
        //     playerMovement.enabled = false;
        //     // playerShooting.enabled = false;
        // }

        // public void RestartLevel()
        // {
        //     EventManager.TriggerEvent("GameOver");
        // }
    }
}