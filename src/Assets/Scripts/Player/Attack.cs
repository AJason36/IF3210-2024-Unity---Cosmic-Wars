using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class Attack : MonoBehaviour
    {
        private StatisticsManager statisticsManager;

        public Camera playerCamera;
        public GameObject projectilePrefab;
        public Transform shootingPoint; // A transform from where projectiles are shot
        public float projectileSpeed = 2000f;
        public Animator animator;
        public float range = 100f;
        public float spreadAngle = 0.5f; 

        void Update()
        {
            statisticsManager.RecordShot();

            if (Input.GetMouseButtonDown(0))
            {
                int weapon = animator.GetInteger("Weapon");
                animator.SetTrigger("Fight");
                if (weapon == 1) {
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, 0));
                    GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    PlayerBullet bulletComponent = projectile.GetComponent<PlayerBullet>();
                    if (bulletComponent != null)
                    {
                        bulletComponent.isShotGun = false;
                    }
                    else
                    {
                        Debug.LogError("PlayerBullet component missing on the projectile prefab!", projectile);
                    }
                    Vector3 targetPoint = ray.origin + ray.direction * range; // Calculate a point in the direction of the ray
                    StartCoroutine(MoveProjectile(projectile, targetPoint));
                } else if (weapon == 2) {
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                    GameObject straightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    PlayerBullet bulletComponent1 = straightProjectile.GetComponent<PlayerBullet>();
                    if (bulletComponent1 != null)
                    {
                        bulletComponent1.isShotGun = true;
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
                        bulletComponent2.isShotGun = true;
                    }
                    Vector3 leftTarget = leftRay.origin + leftRay.direction * range;
                    StartCoroutine(MoveProjectile(leftProjectile, leftTarget));

                    // Shoot right
                    Quaternion rightRotation = Quaternion.Euler(0, spreadAngle, 0); // Rotate the direction right by the spread angle
                    Ray rightRay = new Ray(shootingPoint.position, rightRotation * ray.direction);
                    GameObject rightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(rightRay.direction));
                    PlayerBullet bulletComponent3 = rightProjectile.GetComponent<PlayerBullet>();
                    if (bulletComponent3 != null)
                    {
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
                        bulletComponent5.isShotGun = true;
                    }
                    Vector3 downTarget = downRay.origin + downRay.direction * range;
                    StartCoroutine(MoveProjectile(downProjectile, downTarget));
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
    }
}
