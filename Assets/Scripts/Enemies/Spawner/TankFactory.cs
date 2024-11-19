using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory : EnemySpawnFactory
{
    //[SerializeField] private SpawnTank tankPrefab;

    public override ISpawner GetSpawner(Vector3 position)
    {
        //GameObject instance = Instantiate(tankPrefab.gameObject, position, Quaternion.identity);
        GameObject instance = EnemyPool.instance.GetPooledEnemy();
        if (instance != null) 
        { 
            instance.transform.position = position;
            instance.SetActive(true);
            SpawnTank newTank = instance.GetComponent<SpawnTank>();
            newTank.Spawn();
            return newTank;
        }
        else { return null; }
    }
}
