using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public enum HelicopterState
{
    Tracking,
    Attack,
}


public class HelicopterEnemy : Enemy
{
    [SerializeField] public GameObject target;
    private playerController controller;
    [SerializeField] private HelicopterData data;
    [SerializeField] private GameObject gun;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private GameObject explosion;

    private NavMeshAgent agent;
    private ParticleSystem particleSys;

    private float posRefreshTime;

    private float fireRate;
    private float firingRange;
    private float explosionPower;
    private float explosionRadius;

    private float patrolRadius;

    //runtime values
    private float targetDistance;
    private float attackTimer;
    public bool atDestination;

    [SerializeField] private HelicopterState state;

    public override void EnemyInit()
    {
        base.EnemyInit();

        health = data.health;

        //values about damage 
        damage = data.damage;
        firingRange = data.firingRange;
        fireRate = data.fireRate;
        explosionPower = data.explosionPower;
        explosionRadius = data.explosionRadius;

        //setting some of the navmesh variables 
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.baseOffset = data.heightOffset;

        patrolRadius = data.patrolRadius;
        
        //getting component references
        particleSys = gun.GetComponent<ParticleSystem>();
        controller = target.GetComponentInParent<playerController>();

        // Assign target if null
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null)
            {
                Debug.LogError("No GameObject with tag 'Player' found during EnemyInit!");
                return; // Exit if no target is found
            }
        }

        controller = target.GetComponentInParent<playerController>();




        atDestination = true;

        state = HelicopterState.Tracking;
    }

    public void particleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

        int colEvent = particleSys.GetCollisionEvents(other, colEvents);

        Debug.Log("hit");

        if (other.tag == "Player")
        {
            Debug.Log("Hit Player");
            //deal damage
            //the methods for dealing damage to the player havent been defined yet.
            controller.TakeDamage(damage);
        }

        //this is for if it misses the player. Id thought it'd be cool to add some explosion force if it has a rigidbody
        else if (other.TryGetComponent<Rigidbody>(out Rigidbody rb) == true)
        {
            //this applies a force to other rigibodies making them fly around. 
            //Ill make these easier to edit in the future
            rb.AddExplosionForce(explosionPower, colEvents[0].intersection, explosionRadius);
        }

        //while the collision events are an array the chances of their being multiple collision events are very low. 
        //so instead of running a loop just in case their are multiple I've just chosen the first in the array
        //This might cause issues but I highly doubt there will be enough collision to warrant this being necesarry.
        Instantiate(hitEffect, colEvents[0].intersection, Quaternion.LookRotation(colEvents[0].normal));    
    }

    private void Update()
    {
        Vector2 flatPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 flatTargetPos = new Vector2(agent.destination.x, agent.destination.z);
        targetDistance = Vector2.Distance(flatPosition, flatTargetPos);

        switch (state)
        {
            case HelicopterState.Attack:
                Attack();
                break;
            case HelicopterState.Tracking:
                Tracking();
                break;
        }

        if (targetDistance < agent.stoppingDistance)
        {
            atDestination = true;
        }
        else
        {
            atDestination = false;
        }

        if (targetDistance < firingRange)
        {
            state = HelicopterState.Attack;
        }
        else
        {
            state = HelicopterState.Tracking;
        }
    }

    private void Attack()
    {
        agent.stoppingDistance = 1;

        if (atDestination)
        {
            //helicopter is at it's destination find new waypoint       
            Vector3 targetDest = target.transform.position + new Vector3(Random.Range(-patrolRadius, patrolRadius), 0, Random.Range(-patrolRadius, patrolRadius));
            if (agent.SetDestination(targetDest) == false)
            {
                //if it cannot find a path it will wait until next frame
                //it will then set the destination to itself 
                agent.SetDestination(target.transform.position);
                //this is atDestination stays trye
                Debug.Log("coudl not find dest generated dest: " + targetDest);
            }
        }

        //gun will look towards the target and then play the particle system
        if (attackTimer >= fireRate)
        {
            attackTimer = 0;
            gun.transform.LookAt(target.transform.position);
            particleSys.Play();
            Debug.Log("Fired");
        }

        attackTimer += Time.deltaTime;
    }

    private void Tracking()
    {      
        agent.stoppingDistance = data.stoppingDistance;

        if (atDestination)
        {
            agent.SetDestination(target.transform.position);
            Debug.Log("setting new dest");
        }   
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        EnemyInit();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

   
}
