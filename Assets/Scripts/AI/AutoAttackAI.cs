using UnityEngine;
using UnityEngine.AI;

public class AutoAttackAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject gun;

    //Stats
    public int health;

    //Check for Ground/Obstacles
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attack Player
    public Transform attackPoint;
    public float timeBetweenAttacks;
    private float timeAttackAnim;
    bool alreadyAttacked;

    //States
    public bool isDead;
    [SerializeField] ParticleSystem dieVfx;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Special
    public Material green, red, yellow;
    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("JellyFishGirl").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            //Check if Player in sightrange
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            //Check if Player in attackrange
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (isDead) return;

        if (!walkPointSet) SearchWalkPoint();

        //idle animation
        Run (false);

        //Calculate direction and walk to Point
        if (walkPointSet){
            agent.SetDestination(walkPoint);

            //add walk animation
            Run (true);
            FireWeapon(false);

            //Vector3 direction = walkPoint - transform.position;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }

        //Calculates DistanceToWalkPoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;

            //idle animation
            Run (false);
            FireWeapon(false);

        }
        GetComponent<MeshRenderer>().material = green;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up, 2,whatIsGround))
        walkPointSet = true;
    }
    private void ChasePlayer()
    {
        if (isDead) return;

        agent.SetDestination(player.position);

        GetComponent<MeshRenderer>().material = yellow;
    }
    private void AttackPlayer()
    {
        if (isDead) return;

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked){

            // add attack animation
            Run(false);

            //Attack
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

        GetComponent<MeshRenderer>().material = red;
    }
    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;

        //idle animation
        FireWeapon (true);
        Run(false);

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        print("Enemy Health: " + health);

        if (health <= 0){
            isDead = true;
            Invoke("Destroyy", .5f);
        }
    }
    private void Destroyy()
    {
        Instantiate(dieVfx, transform.position, transform.rotation);
        FindObjectOfType<Moonster>().ModifyMoonsterCount();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    //AI Animation Movement
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        timeAttackAnim = timeBetweenAttacks * 0.7f;
    }

    void FireWeapon(bool activate)
    {
        // activate if true, deactivated if false.
        anim.SetBool("Attack", activate);

        Invoke("StopFire", timeAttackAnim);
    }

    void StopFire()
    {
        anim.SetBool("Attack", false);
    }

    void Run (bool activate)
    {
        // activate if true, deactivated if false.
        anim.SetBool("Run", activate);
    }

}
