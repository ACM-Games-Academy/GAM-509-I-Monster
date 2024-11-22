using UnityEngine;

public class TankFactory : EnemySpawnFactory
{
    public override ISpawner GetSpawner(Vector3 position)
    {
        GameObject instance = PoolingManager.instance.GetTankPool().GetPooledEnemy();
        if (instance != null)
        {
            instance.transform.position = position;
            instance.SetActive(true);

            SpawnTank newTank = instance.GetComponent<SpawnTank>();
            if (newTank != null)
            {
                newTank.Spawn();
                return newTank;
            }
        }
        return null;
    }
}
