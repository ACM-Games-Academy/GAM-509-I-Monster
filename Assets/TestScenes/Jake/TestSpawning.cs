using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawning : MonoBehaviour
{
    [SerializeField] EnemySpawnFactory enemyFactories;

    [SerializeField] Transform spawnTransform;
    private List<GameObject> createdEnemies = new List<GameObject>();

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(StartSpawningEnemy());
    }

    IEnumerator StartSpawningEnemy()
    {
        yield return new WaitForSeconds(3);
        GetSpawnAtPress();
        StartCoroutine(StartSpawningEnemy());

    }

    void GetSpawnAtPress()
    {
        EnemySpawnFactory enemyFactory = enemyFactories;
        ISpawner spawner = enemyFactory.GetSpawner(spawnTransform.position);
        if(spawner is Component component)
        {
            createdEnemies.Add(component.gameObject);
        }
    }
}
