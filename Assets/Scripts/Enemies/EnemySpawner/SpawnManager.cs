using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct SpawnCriteria
{
    public string name;
    public SpawnWaveHandler spawnCriteria;
    public SpawnData spawnData;
    public Transform spawnLocation;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnCriteria> spawnWaves;

    // Update is called once per frame
    private void Update()
    {
        foreach(SpawnCriteria script in spawnWaves) 
        { 
            if(script.spawnCriteria == true)
            {
                Spawn(script.spawnData, script.spawnLocation);
            }
        }
    }

    private void Spawn(SpawnData spawnData, Transform spawnLocation)
    {
        for (int i = 0; i < spawnData.tankAmount; i++)
        {
            Instantiate(spawnData.tankPrefab, spawnLocation.position + randomPoint(spawnData.spawnRadius), Quaternion.identity);
        }

        for (int i = 0;i < spawnData.helicopterAmount; i++)
        {
            Instantiate(spawnData.helicopterPrefab, spawnLocation.position + randomPoint(spawnData.spawnRadius) + Height(spawnData.Height), Quaternion.identity);
        }
    }

    private Vector3 randomPoint(float radius)
    {
        float x = UnityEngine.Random.Range(-radius, radius);

        float z = MathF.Sqrt(MathF.Pow(radius, 2) - MathF.Pow(x, 2));

        return new Vector3(x,0,z);
    }

    private Vector3 Height(float height)
    {
        return new Vector3(0, height, 0);
    }
}
