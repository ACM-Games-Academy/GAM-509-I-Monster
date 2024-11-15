using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct SpawnCriteria
{
    public string name;
    public SpawnWaveHandler spawnCriteria;
    public SpawnData spawnData;
    public Transform spawnLocation;
    public Enemy Enemy;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnCriteria> spawnWaves;

    

    // Update is called once per frame
    private void Update()
    {
 
    }

    private void Spawn(SpawnData spawnData, Transform spawnLocation)
    {
        for (int i = 0; i < spawnData.tankAmount; i++)
        {
            GameObject enemy = Instantiate(spawnData.tankPrefab, spawnLocation.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().initEnemy();
        }

        for (int i = 0;i < spawnData.helicopterAmount; i++)
        {
            Instantiate(spawnData.helicopterPrefab, spawnLocation.position, Quaternion.identity);
        }
    }
}
