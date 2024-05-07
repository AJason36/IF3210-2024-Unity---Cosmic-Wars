using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PetMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent nav;


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
