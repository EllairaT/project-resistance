using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    NavMeshAgent agent;

    public Transform target;

    public LayerMask whatIsPlayer, whatIsStructure;

    //walk 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
    public float timeBetweenAttack;
    bool hasAttacked;

    //states
    public float sightRange = 10f;
    public float attackRange = 10f;

    public bool isInSightRange, isInAttackRange;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //true if any of these are met
        isInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                         Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        while (isInSightRange)
        {
            agent.gameObject.GetComponent<Animator>().SetBool("isInRange", true);
        }

        isInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                          Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
