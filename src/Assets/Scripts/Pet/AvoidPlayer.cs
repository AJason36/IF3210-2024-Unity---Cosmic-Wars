using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare {
    public class AvoidPlayer : MonoBehaviour {
        public Transform playerTransform;
        public Transform boss;
        public NavMeshAgent nav;
        public float safeDistance = 10f;

        void Start() {
            if (nav == null) {
                Debug.LogError("NavMeshAgent is missing on " + gameObject.name);
            }
        }

        void Update() {
            MoveAwayFromPlayer();
        }

        void MoveAwayFromPlayer() {
            if (playerTransform != null && nav != null) {
                Vector3 directionFromPlayer = transform.position - playerTransform.position;
                float distance = directionFromPlayer.magnitude;

                if (distance < safeDistance) {
                    Vector3 moveDirection = directionFromPlayer.normalized;
                    Vector3 newPosition = transform.position + moveDirection * safeDistance;
                    // Ensure the new position is on the NavMesh
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(newPosition, out hit, safeDistance, NavMesh.AllAreas)) {
                        nav.SetDestination(hit.position);
                    } else {
                        Debug.LogError("No valid NavMesh position found within range for avoidance.");
                    }
                } else {
                    nav.SetDestination(boss.position);
                }
            } else {
                Debug.LogError("Player Transform or NavMeshAgent is not assigned on " + gameObject.name);
            }
        }
    }
}