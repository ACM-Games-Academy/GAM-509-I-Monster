using UnityEngine;

//NOTE: THIS IS AN OLD VERSION OF SPAWNING, DON'T USE!

[CreateAssetMenu(fileName = "SpawnData", menuName = "Enemy/Spawn", order = 1)]
public class SpawnData : ScriptableObject
{
    [Header("Amount of Enemies")]
    public int tankAmount;
    public int helicopterAmount;

    [Header("Enemy Prefab")]
    public GameObject tankPrefab;
    public GameObject helicopterPrefab;

    [Header("Measurements")]
    public float spawnRadius;
    public float spawnHeight;
}
