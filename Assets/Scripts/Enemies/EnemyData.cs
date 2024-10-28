using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Data", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Some of these can be left as 0 if not used")]
    public float health;
    public float damage;
}
