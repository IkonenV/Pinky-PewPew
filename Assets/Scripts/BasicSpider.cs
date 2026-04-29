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
    public float biteForce;



    bool isPlayerVisible;
    bool isPlayerInRange;

    public SpiderAttack hitbox;
    public Animator animator;
    EnemyHealth enemyHealth;

    float patrolTimer;
    public float maxPatrolTime = 5f;
    public AudioSource movementAudioSource;
    public float walkPitchRange = 0.2f;
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
        HandleMovementSound();
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
    void HandleMovementSound()
{
    if (movementAudioSource == null) return;

    if (navAgent.velocity.sqrMagnitude > 0.1f && !stunned && enemyHealth.Dead == false)
    {
        // Jos ääni ei vielä soi, laitetaan se päälle
        if (!movementAudioSource.isPlaying)
        {
            movementAudioSource.Play();
        }
        
        movementAudioSource.volume = Mathf.Lerp(0, 1, navAgent.velocity.magnitude / navAgent.speed);
    }
    else
    {
        if (movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }
}

    void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

private void FindPatrolPoint()
{
    Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
    randomDirection += transform.position;

    UnityEngine.AI.NavMeshHit hit;
    

    if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
    {
        currentPatrolPoint = hit.position;
        hasPatrolPoint = true;
        patrolTimer = 0; 
    }
}

void PreformPatrol()
{
    if (!hasPatrolPoint)
    {
        FindPatrolPoint();
        return;
    }

    if (hasPatrolPoint)
    {
        navAgent.SetDestination(currentPatrolPoint);
        patrolTimer += Time.deltaTime;

        float distanceToTarget = Vector3.Distance(transform.position, currentPatrolPoint);


        bool isStuck = (navAgent.velocity.sqrMagnitude < 0.1f && patrolTimer > 1.0f);

        if (distanceToTarget < 1.5f || patrolTimer > maxPatrolTime || isStuck)
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
    }
    void PerformAttack()
    {
        //navAgent.SetDestination(transform.position);

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
