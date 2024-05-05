using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyingPetMovement : MonoBehaviour
{

    public Transform boss;
    public NavMeshAgent nav;

    int abovePositionDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 positionAbove = boss.position;
        positionAbove.y += abovePositionDistance;
        nav.SetDestination(positionAbove);
    }
}
