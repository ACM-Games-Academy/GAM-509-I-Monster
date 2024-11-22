using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health;
    protected float damage;

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
}
