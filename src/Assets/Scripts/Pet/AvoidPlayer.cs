using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare{
public class AvoidPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform boss;
    public NavMeshAgent nav;
    public float moveSpeed = 3f;
    public float safeDistance = 10f;

    private Rigidbody rb; // Rigidbody component of the pet

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Rigidbody is missing on " + gameObject.name);
        }
    }

    void Update()
    {
        MoveAwayFromPlayer();
    }

    void MoveAwayFromPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionFromPlayer = transform.position - playerTransform.position;
            float distance = directionFromPlayer.magnitude;

            // Only move if within a certain distance from the player
            if (distance < safeDistance)
            {
                Vector3 moveDirection = directionFromPlayer.normalized;
                nav.SetDestination(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            }else{
                nav.SetDestination(boss.position);
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned on " + gameObject.name);
        }
    }
}
}