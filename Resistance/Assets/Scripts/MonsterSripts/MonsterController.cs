using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target; //target will always be nexus
    public LayerMask whatIsPlayer, whatIsStructure;
    public Animation anim;

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

    private bool isInSightRange, isInAttackRange;
   
    public List<AnimationClip> movementClips = new List<AnimationClip>();
    public List<AnimationClip> attackClips = new List<AnimationClip>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //true if any of these are met
        isInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                         Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        isInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                          Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        
        if (!isInSightRange)
        {
            WalkForward();
        }
    }

    private void WalkForward()
    {
        Vector3 movement = transform.right * Time.deltaTime * agent.speed;
        anim.Play("walk");
        agent.Move(movement);
        agent.SetDestination(target.position);
        agent.updateRotation = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
