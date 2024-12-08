using UnityEngine;

[CreateAssetMenu(fileName = "HelicopterData" ,menuName = "Enemies/Helicopter" ,order = 1)]
public class HelicopterData : ScriptableObject
{
    
    public float health;
    public float speed;

    [Header("Stopping distance needs to be less than the firingRange")]
    public float stoppingDistance;
    public float patrolRadius;

    [Header("This is the offset from the navMesh")]
    public float heightOffset;

    [Header("Data for projectiles")]
    public float damage;
    public float firingRange;
    public float fireRate;
    public float explosionPower;
    public float explosionRadius;
}
