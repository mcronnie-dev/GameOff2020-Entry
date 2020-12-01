using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TyrantKingAI : MonoBehaviour
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
    [SerializeField] GameObject Splash1;
    [SerializeField] GameObject Splash2;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private Animator _anim;

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
            if (playerInSightRange && !playerInAttackRange) AttackPlayer(false);
            if (playerInAttackRange && playerInSightRange) AttackPlayer(true);
        }
    }

    private void Patroling()
    {
        if (isDead) return;

        // put code for patrolling here
        transform.LookAt(player);
        TriggerShout();
    }
    private void ChasePlayer()
    {
        if (isDead) return;

        // put code for chasePlayer here
    }

    private void AttackPlayer(bool IsNear)
    {
        if (isDead) return;

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        //TriggerNormalAttack();
 
        if (!alreadyAttacked)
        {
            if (IsNear) 
            {
                // add attack animation
                TriggerNormalAttack();

                //Attack
                Instantiate(Splash1, transform.position, transform.rotation);
                Invoke("DestroySplash1", 1.5f);
            }

            else
            {
                // add attack animation
                TriggerJumpAttack();

                //Attack

            }

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

    }
    
    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;

        //idle animation
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        print("Enemy Health: " + health);

        if (health <= 0)
        {
            isDead = true;
            Invoke("Destroyy", 1.5f);
        }
    }

    private void Destroyy()
    {
        print("destroyyyy");
        prevpos = transform.position;
        Instantiate(dieVfx, transform.position, transform.rotation);
 
        Invoke("Destroy1", .5f);
    }

    Vector3 prevpos;
    private void Destroy1()
    {
        Vector3 newpos = prevpos;
        print("destroy 1");
        newpos.y += 1;
        newpos.x += 3;
        Instantiate(dieVfx, newpos, transform.rotation);
        Invoke("Destroy2", .5f);
    }
    private void Destroy2()
    {
        Vector3 newpos = prevpos;
        print("destroy 2");
        newpos.y -= 1;
        newpos.x -= 3;
        Instantiate(dieVfx, newpos, transform.rotation);
        Invoke("Destroy3", .5f);
    }
    private void Destroy3()
    {
        Vector3 newpos = prevpos;
        print("destroy 3");
        newpos.y += 1;
        newpos.x -= 3;
        Instantiate(dieVfx, newpos, transform.rotation);
        Invoke("Destroy4", .5f);
    }
    private void Destroy4()
    {
        Vector3 newpos = prevpos;
        print("destroy 4");
        newpos.y -= 1;
        newpos.x += 3;
        Instantiate(dieVfx, newpos, transform.rotation);
        Invoke("Destroy5", .5f);
    }
    private void Destroy5()
    {
        Vector3 newpos = prevpos;
        print("destroy 5");
        Instantiate(dieVfx, newpos, transform.rotation);
        Destroy(gameObject);
    }

    private void DestroySplash1()
    {
        Destroy(Splash1);
    }



    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        TriggerGetHit(col);   
    }

    void TriggerGetHit(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            TriggerGetHit();
        }
    }

    void ResetAllTrigger()
    {
        _anim.ResetTrigger("NormalAttack");
        _anim.ResetTrigger("JumpAttack");
        _anim.ResetTrigger("GetHit");
        _anim.ResetTrigger("Shout");
    }

    void TriggerShout()
    {
        ResetAllTrigger();
        _anim.SetTrigger("Shout");
    }

    void GenerateRandAtk()
    {
        // Put code here for switch random attack.
    }


    // Triggers Normal Attack if IsPlayerNear is `true`
    void TriggerNormalAttack()
    {
        ResetAllTrigger();
        //can be interrupted
        _anim.SetTrigger("NormalAttack");
    }

    
    // Triggers Jump Attack if IsPlayerNear is `true`
    void TriggerJumpAttack()
    {
        ResetAllTrigger();
        //can be interrupted
        _anim.SetTrigger("JumpAttack");
    }

    // Triggers GetHit if IsInterrupted is `true`
    void TriggerGetHit()
    {
        ResetAllTrigger();
        _anim.SetTrigger("GetHit");
    }

}
