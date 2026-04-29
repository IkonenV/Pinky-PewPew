using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform playerTransform;
    public Transform firePoint;
    public GameObject projectilePrefab;

    public LayerMask terrainLayer;
    public LayerMask playerLayerMask;

    public float patrolRadius = 10f;
    Vector3 currentPatrolPoint;
    bool hasPatrolPoint;

    public float attackCooldown = 1f;
    bool isOnAttackCooldown;
    public float forwardShotForce = 10f;
    public float verticalShotForce = 5f;

    public float visionRange = 20f;
    public float engagementRange = 10f;
    public bool hostile;
    public Animator animator;



    bool isPlayerVisible;
    bool isPlayerInRange;
    EnemyHealth enemyHealth;
    public AudioClip[] shootingSounds;
    public bool stunned;
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
    public void FireProjectile()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(shootingSounds, transform, 0.25f);
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        rb.AddForce(transform.up* verticalShotForce, ForceMode.Impulse);

        Destroy(bullet, 3f);
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
        navAgent.SetDestination(transform.position);

        if(playerTransform != null)
        {
            transform.LookAt(playerTransform);

            if (!isOnAttackCooldown)
            {
                StartCoroutine(AttackCooldownRoutine());
                animator.SetTrigger("Shoot");
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
