using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int tankCount;
        public int helicopterCount;
    }

    // Array of waves
    [SerializeField] private Wave[] waves; 

    // Spawn delay
    [SerializeField] private float spawnDelay = 0;

    // Array of spawn positions
    [SerializeField] private Transform[] spawnPositions;

    // Separate factories for tanks and helicopters
    [SerializeField] private EnemySpawnFactory tankFactory;
    [SerializeField] private EnemySpawnFactory helicopterFactory;

    private List<GameObject> createdEnemies = new List<GameObject>();
    private int currentWaveIndex = 0;

    public bool killAllEnemies = false;


    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log($"Starting Wave {currentWaveIndex + 1}");

            // Spawn tanks
            for (int i = 0; i < currentWave.tankCount; i++)
            {
                SpawnEnemy(tankFactory);
                yield return new WaitForSeconds(spawnDelay); // Delay each tank spawn
            }

            // Spawn helicopters
            for (int i = 0; i < currentWave.helicopterCount; i++)
            {
                SpawnEnemy(helicopterFactory);
                yield return new WaitForSeconds(spawnDelay); // Delay each helicopter spawn
            }

            // Wait until all enemies from this wave are destroyed
            yield return new WaitUntil(() => createdEnemies.Count == 0);

            Debug.Log($"Wave {currentWaveIndex + 1} completed!");
            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }

    private void SpawnEnemy(EnemySpawnFactory factory)
    {
        if (factory == null)
        {
            Debug.LogError("Enemy factory is null!");
            return;
        }

        // Choose a random spawn position for this specific enemy
        Transform randomSpawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        // Get the spawner from the factory at the chosen position
        ISpawner spawner = factory.GetSpawner(randomSpawnPosition.position);
        if (spawner is Component component)
        {
            GameObject enemy = component.gameObject;
            enemy.SetActive(true);

            // Register the enemy
            createdEnemies.Add(enemy);
        }
    }

    // Place holder to destroy all enemeis
    private void Update()
    {
        if (killAllEnemies)
        {
            foreach (var enemy in createdEnemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }

 
            createdEnemies.Clear();
            killAllEnemies = false;
        }
    }
}