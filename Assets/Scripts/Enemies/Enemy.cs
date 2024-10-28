using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyData data;

    protected float health;
    protected float damage;

    private void OnEnable()
    {
        health = data.health;
        damage = data.damage;
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
}
