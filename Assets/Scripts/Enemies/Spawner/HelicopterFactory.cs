using UnityEngine;

public class HelicopterFactory : EnemySpawnFactory
{
    public override ISpawner GetSpawner(Vector3 position)
    {
        GameObject instance = PoolingManager.instance.GetHelicopterPool().GetPooledEnemy();
        if (instance != null)
        {
            instance.transform.position = position;
            instance.SetActive(true);

            SpawnHelicopter newHelicopter = instance.GetComponent<SpawnHelicopter>();
            if (newHelicopter != null)
            {
                newHelicopter.Spawn();
                return newHelicopter;
            }
        }
        return null;
    }
}
