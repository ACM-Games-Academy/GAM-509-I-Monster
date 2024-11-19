using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelicopter : MonoBehaviour, ISpawner
{
    [SerializeField] private string enemyName = "Helicopter";
    public string EnemyName
    { get => enemyName; set => enemyName = value; }

    public void Spawn()
    {
        gameObject.name = enemyName;
    }
}
