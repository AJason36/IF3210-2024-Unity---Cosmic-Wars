using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        // private PlayerHealth playerHealth;
        // Animator anim;

        // LevelManager lm;
        // private UnityEvent listener;

        // void Awake ()
        // {
        //     playerHealth = FindObjectOfType<PlayerHealth>();
        //     anim = GetComponent <Animator> ();
        //     lm = FindObjectOfType<LevelManager>();
        //     EventManager.StartListening("GameOver", ShowGameOver);
        // }

        // void OnDestroy()
        // {
        //     EventManager.StopListening("GameOver", ShowGameOver);
        // }

        // void ShowGameOver()
        // {
        //     anim.SetBool("GameOver", true);
        // }

        // private void ResetLevel()
        // {
        //     ScoreManager.score = 0;
        //     LevelManager lm = FindObjectOfType<LevelManager>();
        //     lm.LoadInitialLevel();
        //     anim.SetBool("GameOver", false);
        //     playerHealth.ResetPlayer();
        // }
        public PlayerHealth playerHealth;
        public PlayerMovement playerMovement;
        public float restartDelay = 5f;

        Animator anim;
        float restartTimer;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if(playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("GameOver");
                restartTimer += Time.deltaTime;
                if(restartTimer >= restartDelay)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Application.LoadLevel("GameOverScene");
                }
            }
        }

    }
}