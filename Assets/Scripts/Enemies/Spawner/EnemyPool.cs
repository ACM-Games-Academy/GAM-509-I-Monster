using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // Pool properties
    private List<GameObject> pooledEnemies = new List<GameObject>();
    private int amountToPool;
    private GameObject enemyPrefab;

    // Constructor to initialize the pool
    public void Initialize(GameObject prefab, int initialPoolSize)
    {
        enemyPrefab = prefab;
        amountToPool = initialPoolSize;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pooledEnemies.Add(enemy);
        }
    }

    // Get an inactive enemy from the pool
    public GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in pooledEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
     
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        pooledEnemies.Add(newEnemy);
        return newEnemy;
    }
}
