using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class HelicopterEnemy : Enemy
{
    [SerializeField] private GameObject target;
    [SerializeField] private HelicopterData data;
    [SerializeField] private GameObject gun;
    [SerializeField] private ParticleSystem hitEffect;

    private NavMeshAgent agent;
    private CapsuleCollider firingArea;
    private ParticleSystem particleSys;

    private float posRefreshTime;

    private float fireRate;
    private float explosionPower;
    private float explosionRadius;

    private Coroutine helicopterMovement;
    private Coroutine helicopterAttack;

    protected override void Start()
    {
        base.Start();
        health = data.health;
        posRefreshTime = data.updateTargetRefresh;

        damage = data.damage;
        fireRate = data.fireRate;
        explosionPower = data.explosionPower;
        explosionRadius = data.explosionRadius;

        //setting some of the navmesh variables 
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = data.stoppingDistance;
        agent.baseOffset = data.heightOffset;

        //setting up the trigger area
        //I thought using a trigger instead of checking the distance every frame would run more efficiently 
        firingArea = GetComponentInChildren<CapsuleCollider>();
        firingArea.radius = data.firingRange;
        firingArea.height = 2 * data.heightOffset + 2 * data.firingRange;  //this increases the height and then increases it again by the radius. This is so the curved part is below the floor making it more similar to a cylinder.
        

        particleSys = gun.GetComponent<ParticleSystem>();

        //setting some of the coroutines an starting the movement coroutine
        helicopterMovement = StartCoroutine(HelicopterTrack());
    }

    protected override void Update()
    {
        base.Update();
    }

    //this will run for the entire time the object is alive it just makes the helicopter follow the player
    private IEnumerator HelicopterTrack()
    {
        while (true)
        {
            agent.SetDestination(target.transform.position);
            yield return new WaitForSeconds(posRefreshTime);
        }
    }
    
    private IEnumerator HelicopterAttack()
    {
        while (true)
        {
            //gun will look towards the target and then play the particle system
            gun.transform.LookAt(target.transform.position);
            particleSys.Play();
            Debug.Log("Fired");
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

        if (other == target)
        {
            //deal damage
            //the methods for dealing damage to the player havent been defined yet.
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

    //These controls when the attack coroutine starts and finishes
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger");
        if (other.gameObject == target)  //if the object inside of the trigger is the player
        {
            Debug.Log("Player entered firing range");
            helicopterAttack = StartCoroutine(HelicopterAttack());
            //so i started firing
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            Debug.Log("Player exited firing range");
            StopCoroutine(helicopterAttack);
            //so i stopped firing
        }
    }

    

    protected override void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
