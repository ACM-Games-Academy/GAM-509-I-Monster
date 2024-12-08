using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fuelContainer : Enemy
{
    [SerializeField] fuelContainerData data;
    private float damageRange;

    [SerializeField] GameObject explosionEffect;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        health = data.health;
        damage = data.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Explosions but notright ");
        //not the player and over speed of 1 
        //it checks if the speed is over 1 to account for jitters or very slight movement 
        //it also makes sure the containers isnt kinematic as it's kinematic when its pickedup
        if (!collision.gameObject.CompareTag("Player") && collision.relativeVelocity.sqrMagnitude > 5 && !rb.isKinematic)
        {
            Explode();

            Debug.Log("Explosion");
        }
    }

    private void Explode()
    {
        Collider[] hitObjects = null;
        hitObjects = new Collider[Physics.OverlapSphereNonAlloc(transform.position, damageRange, hitObjects)];

        foreach (Collider collider in hitObjects)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemyScript))
            {
                enemyScript.giveDamage(damage);
                
            }
        }

        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    //I added a destroy because the inherited enemy script is to be used for object pooling 
    //since this barrel isnt being pooled it will destroy itself when it gets disabled
    //this also means when it reaches 0 health it will do the same 
    private void OnDisable()
    {
        Instantiate(explosionEffect);
        Destroy(this.gameObject);
    }
}
