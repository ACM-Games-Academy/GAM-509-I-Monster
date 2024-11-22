using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGun : MonoBehaviour
{
    [SerializeField] HelicopterEnemy helicopter;

    private void OnParticleCollision(GameObject other)
    {
        helicopter.particleCollision(other);
    }
}
