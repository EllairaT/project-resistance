using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class MonsterController : NetworkBehaviour
{
    public float monsterFieldOfView = 90f;
    public Card stats;
    public Transform target; 
    public LayerMask whatIsPlayer, whatIsStructure;
    NavMeshAgent agent;

    public Animator anim;

    //attack
    public float timeBetweenAttack;
    bool hasAttacked;

    //states
    public float sightRange = 30f;
    public float attackRange = 10f;

   // private , isInAttackRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = MonsterManager.instance.nexus.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Debug.DrawRay(transform.position, target.position, Color.red);

        if (IsInSight())
        {
            WalkForward();
        }
        else if(IsInSight() && distance <= sightRange)
        {
            StopMoving();
        }

    }

    private bool IsInSight()
    {
        //get forward vector
        //get direction between enemy and player
        //get angle
        bool isInSightRange;
        Vector3 targetDirection = target.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        if(angle < monsterFieldOfView)
        {
            isInSightRange = true;
        }
        else
        {
            isInSightRange = false;
        }

        return isInSightRange;
    }

    private void WalkForward()
    {
        anim.SetBool("isMoving", true);
        Vector3 movement = transform.right * Time.deltaTime * agent.speed;
        agent.SetDestination(target.position);
  
        agent.updateRotation = false;
    }

    public void PlaySpawnAnim()
    {
        anim.SetTrigger("Spawn");
    }

    public void StopMoving()
    {
        anim.SetBool("isMoving", false);
        agent.velocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
