using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    protected float damage;
    public bool allowPhysicsDamage = true;

    public virtual void EnemyInit()
    {

    }

    public void giveDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (allowPhysicsDamage)
        {
            giveDamage(collision.relativeVelocity.magnitude * (collision.collider.bounds.size.magnitude / 2));
        }
    }
}
