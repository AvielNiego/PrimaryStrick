using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class EnemyAi : MonoBehaviour
{
    private static readonly int MOVE = Animator.StringToHash("move");
    private static readonly int ATTACK = Animator.StringToHash("attack");

    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 10f;


    private NavMeshAgent _navMeshAgent;
    private Animator animator;

    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked;
    [SerializeField] private float turnSpeed = 5f;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else
        {
            CheckIfProvoked();
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        else
        {
            ChaseTarget();
        }
    }

    private void AttackTarget()
    {
        animator.SetBool(ATTACK, true);
    }

    private void ChaseTarget()
    {
        animator.SetBool(ATTACK, false);
        animator.SetTrigger(MOVE);
        _navMeshAgent.SetDestination(target.position);
    }

    private void CheckIfProvoked()
    {
        if (isProvoked)
        {
            return;
        }


        if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _navMeshAgent.stoppingDistance);
    }
}