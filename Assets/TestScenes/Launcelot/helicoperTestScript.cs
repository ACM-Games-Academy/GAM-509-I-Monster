using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicoperTestScript : MonoBehaviour
{
    public HelicopterEnemy heli;

    private void Start()
    {
        heli.EnemyInit();
    }
}
