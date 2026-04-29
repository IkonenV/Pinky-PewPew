using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BigSpider : MonoBehaviour
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



    bool isPlayerVisible;
    bool isPlayerInRange;

    AudioSource audioSource;

    public BigSpiderAttack hitbox;
    public Animator animator;
    EnemyHealth enemyHealth;
    bool playingWalkSound = false;
    public bool stunned;
    float patrolTimer;
    public float maxPatrolTime = 5f;
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
        audioSource = GetComponent<AudioSource>();
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

    Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


    UnityEngine.AI.NavMeshHit hit;
    if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
    {
        currentPatrolPoint = hit.position;
        hasPatrolPoint = true;
    }
}
void PreformPatrol()
{
    if (!hasPatrolPoint)
    {
        FindPatrolPoint();
        patrolTimer = 0; 
    }

    if(hasPatrolPoint)
    {
        navAgent.SetDestination(currentPatrolPoint);
        patrolTimer += Time.deltaTime;

        if(Vector3.Distance(transform.position, currentPatrolPoint) < 1.5f || patrolTimer > maxPatrolTime)
        {
            hasPatrolPoint = false;
        }
    }
}
    void PerformChase()
    {
        if(playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
        if(!playingWalkSound)
        PlayWalk();
    }
    void PerformAttack()
    {
        navAgent.SetDestination(transform.position);
        playingWalkSound = false;

        if(playerTransform != null)
        {
            transform.LookAt(playerTransform);

            if(playingWalkSound)
            StopWalk();

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
    public void PlayWalk()
    {
        audioSource.Play();
        playingWalkSound = true;
    }
    public void StopWalk()
    {
        audioSource.Stop();
        playingWalkSound = false;
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

