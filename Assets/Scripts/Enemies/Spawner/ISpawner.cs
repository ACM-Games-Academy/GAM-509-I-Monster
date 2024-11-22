using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public string EnemyName { get; set; }

    public void Spawn();
}
