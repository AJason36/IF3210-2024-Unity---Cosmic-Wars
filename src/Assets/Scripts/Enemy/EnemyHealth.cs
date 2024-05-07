using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;

        public Slider healthSlider;
        public bool isBoss = false;

        int currentHealth;
        Animator anim;
        AudioSource enemyAudio;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            enemyMovement = this.GetComponent<EnemyMovement>();
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update ()
        {
            if (IsDead())
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        public void TakeDamage (int amount)
        {
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
                else
                {
                    enemyMovement.GoToPlayer();
                }

                if(isBoss){
                    healthSlider.value = currentHealth;
                }
            }
        }

        void Death ()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger ("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }

        public void StartSinking ()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);

            ScoreManager.score += scoreValue;
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }
    }
}