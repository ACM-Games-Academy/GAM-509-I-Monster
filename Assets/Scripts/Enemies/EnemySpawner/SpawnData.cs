using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Enemy/SpawnEvent", order = 1)]

public class SpawnData : ScriptableObject
{
    [Header("Amount of Enemies")]
    public int tankAmount;
    public int helicopterAmount;

    [Header("Enemy Prefab")]
    public GameObject tankPrefab;
    public GameObject helicopterPrefab;

    [Header("Radius")]
    public float spawnRadius;

    [Header("Height")]
    public float Height;
}
