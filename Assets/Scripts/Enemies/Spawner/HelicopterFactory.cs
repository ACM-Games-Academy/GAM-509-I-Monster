using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFactory : EnemySpawnFactory
{
    //[SerializeField] private SpawnHelicopter helicopterPrefab;

    public override ISpawner GetSpawner(Vector3 position)
    {
        //GameObject instance = Instantiate(helicopterPrefab.gameObject, position, Quaternion.identity);
        GameObject instance = EnemyPool.instance.GetPooledEnemy();
        if (instance != null) 
        { 
            instance.transform.position = position;
            instance.SetActive(true);
            SpawnHelicopter newHelicopter = instance.GetComponent<SpawnHelicopter>();
            newHelicopter.Spawn();
            return newHelicopter;
        }

        else { return null; }
    }
}
