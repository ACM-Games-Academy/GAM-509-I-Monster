using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawnFactory : MonoBehaviour
{
    public abstract ISpawner GetSpawner(Vector3 position);
}
