using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyPetMovement : MonoBehaviour
{
    public Transform boss;
    public NavMeshAgent nav;


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(boss.position);
    }
}