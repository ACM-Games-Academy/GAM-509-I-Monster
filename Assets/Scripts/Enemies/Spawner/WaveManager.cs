using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

    // Helicopter height offset
    [SerializeField] private float helicopterHeightOffset = 10.0f;

    public EventHandler levelComplete;

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

            // Determine the maximum number of spawns for this wave
            int maxCount = Mathf.Max(currentWave.tankCount, currentWave.helicopterCount);

            for (int i = 0; i < maxCount; i++)
            {
                // Spawn a tank if within the tank count
                if (i < currentWave.tankCount)
                {
                    SpawnEnemy(tankFactory, false);
                }

                // Spawn a helicopter if within the helicopter countw
                if (i < currentWave.helicopterCount)
                {
                    SpawnEnemy(helicopterFactory, true);
                }

                yield return new WaitForSeconds(spawnDelay); // Delay after each spawn iteration
            }

            // Wait until all enemies from this wave are destroyed
            yield return new WaitUntil(() => createdEnemies.Count == 0);

            Debug.Log($"Wave {currentWaveIndex + 1} completed!");
            currentWaveIndex++;

            yield return new WaitForSeconds(20);
        }


        Debug.Log("All waves completed!");
        levelComplete.Invoke(this, EventArgs.Empty);
    }


    private void SpawnEnemy(EnemySpawnFactory factory, bool isHelicopter)
    {

        if (factory == null)
        {
            Debug.LogError("Enemy factory is null!");
            return;
        }

        // Choose a random spawn position for this specific enemy
        Transform randomSpawnPosition = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)];

        // Adjust Y-level for helicopters
        Vector3 spawnPosition = randomSpawnPosition.position;
        if (isHelicopter)
        {
            spawnPosition.y += helicopterHeightOffset; // Add height offset for helicopters
        }

        // Spawn enemy
        ISpawner spawner = factory.GetSpawner(spawnPosition);
        if (spawner is Component component)
        {
            GameObject enemy = component.gameObject;
            enemy.SetActive(true);

            // Initialize the enemy if it's a helicopter
            if (isHelicopter && enemy.TryGetComponent<HelicopterEnemy>(out HelicopterEnemy helicopterEnemy))
            {
                helicopterEnemy.EnemyInit(); // Call instance method properly
            }

            createdEnemies.Add(enemy);
        }
    }

    // Placeholder to destroy all enemies
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


        //quick update - Launcelot
        foreach (var enemy in createdEnemies)
        {
            if (enemy.active == false)
            {
                createdEnemies.Remove(enemy);
            }
        }
    }
}
