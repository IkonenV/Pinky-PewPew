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
    public void FireProjectile()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(shootingSounds, transform, 0.5f);
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        rb.AddForce(transform.up* verticalShotForce, ForceMode.Impulse);

        Destroy(bullet, 3f);
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
