using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class Attack : MonoBehaviour
    {
        private StatisticsManager statisticsManager;
        private int totalAttackOrbs;
        private int damage;
        public int initialDamage=40;
        public Camera playerCamera;
        public GameObject projectilePrefab;
        public Transform shootingPoint; // A transform from where projectiles are shot
        public float projectileSpeed = 2000f;
        public Animator animator;
        public float range = 100f;
        public float spreadAngle = 0.5f;
        public Collider meleeCollider;
        public bool enemyInRange;
        public GameObject enemy;
        public float gunCooldown = 0.5f; 
        public float meleeCooldown = 0.75f;
        public float shotgunCooldown = 1.0f;
        private float lastAttackTime;
        public ParticleSystem gunEffect;
        public ParticleSystem shotgunEffect;
        public bool isOneHitKill = false;
        
        void Start()
        {
            statisticsManager = StatisticsManager.Instance;
            totalAttackOrbs = 0;
            initialDamage = 40;
            damage = initialDamage; 
            meleeCollider = GetComponent<Collider>();
        }

        void Update()
        {
            if(isOneHitKill){
                damage = 9999;
            }
            if (Input.GetMouseButtonDown(0)  && Time.time > lastAttackTime)
            {
                int weapon = animator.GetInteger("Weapon");
                
                if (weapon == 1 && Time.time > lastAttackTime + gunCooldown) {
                    animator.SetTrigger("Fight");
                    lastAttackTime = Time.time;
                    statisticsManager.RecordShot();
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, 1 * Screen.height / 2));
                    GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    PlayerBullet bulletComponent = projectile.GetComponent<PlayerBullet>();
                    
                    if (bulletComponent != null)
                    {
                        bulletComponent.damage = damage; 
                        bulletComponent.isShotGun = false;
                        if (gunEffect != null) {
                            ParticleSystem effectInstance = Instantiate(gunEffect, shootingPoint.position, Quaternion.identity);  // Instantiate effect
                            effectInstance.transform.parent = projectile.transform; 
                            if(!effectInstance.isPlaying){
                                effectInstance.Play();
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("PlayerBullet component missing on the projectile prefab!", projectile);
                    }
                    Vector3 targetPoint = ray.origin + ray.direction * range; // Calculate a point in the direction of the ray
                    StartCoroutine(MoveProjectile(projectile, targetPoint));
                } else if (weapon == 2 && Time.time > lastAttackTime + shotgunCooldown) {
                    animator.SetTrigger("Fight");
                    lastAttackTime = Time.time;
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, 1*Screen.height / 3));
                    GameObject straightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    PlayerBullet bulletComponent1 = straightProjectile.GetComponent<PlayerBullet>();
                    
                    if (bulletComponent1 != null)
                    {
                        bulletComponent1.damage = damage;
                        bulletComponent1.isShotGun = true;;
                        if (gunEffect != null) {
                            ParticleSystem effectInstance = Instantiate(shotgunEffect, shootingPoint.position, Quaternion.identity);  // Instantiate effect
                            // effectInstance.transform.parent = straightProjectile.transform; 
                            if(!effectInstance.isPlaying){
                                effectInstance.Play();
                            }
                        }
                    }
                    Vector3 straightTarget = ray.origin + ray.direction * range;
                    StartCoroutine(MoveProjectile(straightProjectile, straightTarget));

                    // Shoot left
                    Quaternion leftRotation = Quaternion.Euler(0, -spreadAngle, 0); // Rotate the direction left by the spread angle
                    Ray leftRay = new Ray(shootingPoint.position, leftRotation * ray.direction);
                    GameObject leftProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(leftRay.direction));
                    PlayerBullet bulletComponent2 = leftProjectile.GetComponent<PlayerBullet>();
                    
                    if (bulletComponent2 != null)
                    {
                        bulletComponent2.damage = damage;
                        bulletComponent2.isShotGun = true;
                    }
                    Vector3 leftTarget = leftRay.origin + leftRay.direction * range;
                    StartCoroutine(MoveProjectile(leftProjectile, leftTarget));

                    // Shoot right
                    Quaternion rightRotation = Quaternion.Euler(0, spreadAngle, 0); // Rotate the direction right by the spread angle
                    Ray rightRay = new Ray(shootingPoint.position, rightRotation * ray.direction);
                    GameObject rightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(rightRay.direction));
                    PlayerBullet bulletComponent3 = rightProjectile.GetComponent<PlayerBullet>();
                    bulletComponent3.damage = damage;
                    if (bulletComponent3 != null)
                    {
                        bulletComponent3.damage = damage;
                        bulletComponent3.isShotGun = true;
                    }
                    Vector3 rightTarget = rightRay.origin + rightRay.direction * range;
                    StartCoroutine(MoveProjectile(rightProjectile, rightTarget));

                    // Shoot right
                    Quaternion upRotation = Quaternion.Euler(spreadAngle, 0, 0); // Rotate the direction right by the spread angle
                    Ray upRay = new Ray(shootingPoint.position, upRotation * ray.direction);
                    GameObject upProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(upRay.direction));
                    PlayerBullet bulletComponent4 = upProjectile.GetComponent<PlayerBullet>();
                    if (bulletComponent4 != null)
                    {
                        bulletComponent4.damage = damage;
                        bulletComponent4.isShotGun = true;
                    }
                    Vector3 upTarget = upRay.origin + upRay.direction * range;
                    StartCoroutine(MoveProjectile(upProjectile, upTarget));

                    // Shoot right
                    Quaternion downRotation = Quaternion.Euler(-spreadAngle, 0, 0); // Rotate the direction right by the spread angle
                    Ray downRay = new Ray(shootingPoint.position, downRotation * ray.direction);
                    GameObject downProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(downRay.direction));
                    PlayerBullet bulletComponent5 = downProjectile.GetComponent<PlayerBullet>();
                    if (bulletComponent5 != null)
                    {
                        bulletComponent5.damage = damage;
                        bulletComponent5.isShotGun = true;
                    }
                    Vector3 downTarget = downRay.origin + downRay.direction * range;
                    StartCoroutine(MoveProjectile(downProjectile, downTarget));
                }
                else if (weapon == 3 && Time.time > lastAttackTime + meleeCooldown) 
                {
                    animator.SetTrigger("Fight");
                    lastAttackTime = Time.time;
                    PerformMeleeAttack((int)(damage*1.5)); 
                }else if(weapon ==4 && Time.time > lastAttackTime + meleeCooldown){
                    animator.SetTrigger("Fight");
                    lastAttackTime = Time.time;
                    PerformMeleeAttack(damage/2);
                }
            }
        }

        IEnumerator MoveProjectile(GameObject projectile, Vector3 target) {
            while (projectile != null && Vector3.Distance(projectile.transform.position, target) > 0.1f) {
                projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, target, projectileSpeed * Time.deltaTime);
                yield return null;
            }

            if (projectile != null) {
                Destroy(projectile);
            }
        }

        public void DrinkAttackOrbs()
        {
            if(totalAttackOrbs < 15)
            {
                totalAttackOrbs += 1;
                damage += (int)(0.1*initialDamage);
            }
        }

        private void PerformMeleeAttack(int damage)
        {
            if(enemy!=null && enemyInRange)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if(enemyHealth!=null && enemyHealth.currentHealth>0)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }

        void OnTriggerEnter (Collider other)
        {
            if(other.gameObject.CompareTag("Enemy")||other.gameObject.CompareTag("Jendral"))
            {
                enemy = other.gameObject;
                enemyInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            if(other.gameObject.CompareTag("Enemy")||other.gameObject.CompareTag("Jendral"))
            {
                enemyInRange = false;
            }
        }

    }
}
