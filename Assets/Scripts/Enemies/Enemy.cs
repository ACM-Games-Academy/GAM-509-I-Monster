using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health;
    protected float damage;

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public void giveDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
