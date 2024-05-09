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
        public bool isPet = false;

        public int currentHealth;
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
            currentHealth = startingHealth;
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
            if (IsDead() && !isPet)
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -0.35f)
                {
                    Destroy(gameObject);
                }
            }
            if(isPet && IsDead()){
                Destroy(gameObject);
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
                if (!isPet){
                    enemyAudio.Play();
                }
                currentHealth -= amount;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                }

                if (IsDead() && !isPet)
                {
                    Death();
                }
                else if(!isPet)
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
            // EventManager.TriggerEvent("Sound", this.transform.position);
            if(!isPet){
                Debug.Log("Masuk ke sini deadnya");
                anim.SetTrigger ("Dead");
                enemyAudio.clip = deathClip;
                enemyAudio.Play ();
            }
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