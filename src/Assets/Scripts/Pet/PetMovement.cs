using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PetMovement : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public NavMeshAgent nav;


    void Awake() 
    {
      player = GameObject.FindGameObjectWithTag("Player");
      playerTransform = player.transform;
      
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(playerTransform.position);
    }
}
