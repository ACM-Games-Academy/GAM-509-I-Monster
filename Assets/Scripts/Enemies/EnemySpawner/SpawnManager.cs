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
            
        }
    }
}
