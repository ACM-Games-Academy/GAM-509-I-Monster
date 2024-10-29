using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelicopterData" ,menuName = "Enemies/Helicopter" ,order = 1)]
public class HelicopterData : ScriptableObject
{
    public float health;
    public float speed;
    public float updateTargetRefresh;
    public float stoppingDistance;   

    [Header("This is the offset from the navMesh")]
    public float heightOffset;

    [Header("Data for projectiles")]
    public float damage;
    public float firingRange;
    public float fireRate;
    public float explosionPower;
    public float explosionRadius;
}
