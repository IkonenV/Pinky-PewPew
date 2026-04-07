using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicSpider : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform playerTransform;


    public LayerMask terrainLayer;
    public LayerMask playerLayerMask;

    public float patrolRadius = 10f;
    Vector3 currentPatrolPoint;
    bool hasPatrolPoint;

    public float attackCooldown = 1f;
    bool isOnAttackCooldown;
    public float visionRange = 20f;
    public float engagementRange = 10f; 
    public bool hostile; 
    bool stunned;



    bool isPlayerVisible;
    bool isPlayerInRange;

    public SpiderAttack hitbox;
    public Animator animator;
    EnemyHealth enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
        if(enemyHealth.hostile == true && hostile == false)
        {
            hostile = true;
        }
        if(enemyHealth.Dead == true)
        {
            navAgent.destination = transform.position;
        }
    }
    


    void UpdateBehaviourState()
    {
        if(!hostile)
        {
            PreformPatrol();
        }
        else if (hostile && !isPlayerInRange && !stunned)
        {
            PerformChase();
        }
        else if(hostile && isPlayerInRange && !stunned)
        {
            PerformAttack();
        }
    }

    void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }
    void PreformPatrol()
    {
        if (!hasPatrolPoint)
        FindPatrolPoint();

        if(hasPatrolPoint)
        navAgent.SetDestination(currentPatrolPoint);

        if(Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
        hasPatrolPoint = false;
    }
    void PerformChase()
    {
        if(playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
    }
    void PerformAttack()
    {
        navAgent.SetDestination(transform.position);

        if(playerTransform != null)
        {
            transform.LookAt(playerTransform);

            if (!isOnAttackCooldown)
            {
                animator.SetTrigger("Bite");
                StartCoroutine(AttackCooldownRoutine());
            }
        }
    }
    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }
    public void StopMoving()
    {
        stunned = true;
        navAgent.destination = transform.position;
    }
    public void StartMoving()
    {
        stunned = false;
    }


}
