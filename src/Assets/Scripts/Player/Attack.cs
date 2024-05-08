using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class Attack : MonoBehaviour
    {
        public Camera playerCamera;
        public GameObject projectilePrefab;
        public Transform shootingPoint; // A transform from where projectiles are shot
        public float projectileSpeed = 2000f;
        public Animator animator;
        public float range = 100f;
        public float spreadAngle = 15f; 

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                int weapon = animator.GetInteger("Weapon");
                animator.SetTrigger("Fight");
                if (weapon == 1) {
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                    GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    Vector3 targetPoint = ray.origin + ray.direction * range; // Calculate a point in the direction of the ray
                    StartCoroutine(MoveProjectile(projectile, targetPoint));
                } else if (weapon == 2) {
                    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                    GameObject straightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(ray.direction));
                    Vector3 straightTarget = ray.origin + ray.direction * range;
                    StartCoroutine(MoveProjectile(straightProjectile, straightTarget));

                    // Shoot left
                    Quaternion leftRotation = Quaternion.Euler(0, -spreadAngle, 0); // Rotate the direction left by the spread angle
                    Ray leftRay = new Ray(shootingPoint.position, leftRotation * ray.direction);
                    GameObject leftProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(leftRay.direction));
                    Vector3 leftTarget = leftRay.origin + leftRay.direction * range;
                    StartCoroutine(MoveProjectile(leftProjectile, leftTarget));

                    // Shoot right
                    Quaternion rightRotation = Quaternion.Euler(0, spreadAngle, 0); // Rotate the direction right by the spread angle
                    Ray rightRay = new Ray(shootingPoint.position, rightRotation * ray.direction);
                    GameObject rightProjectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(rightRay.direction));
                    Vector3 rightTarget = rightRay.origin + rightRay.direction * range;
                    StartCoroutine(MoveProjectile(rightProjectile, rightTarget));
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