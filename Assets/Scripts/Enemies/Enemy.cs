using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health;
    protected float damage;

    public void Start()
    {
        
    }
    public virtual void initEnemy()
    {
        //assign enemy values here i your specific enemy class
    }

    private void giveDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
