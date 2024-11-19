using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTank : MonoBehaviour, ISpawner
{
    [SerializeField] private string enemyName = "Tank";
    public string EnemyName 
    { get => enemyName; set => enemyName= value; }

    public void Spawn()
    {
        gameObject.name = enemyName;
    }
}
    