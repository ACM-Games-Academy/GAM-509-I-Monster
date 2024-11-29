using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModel
{
    float health = 100;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    // Update is called once per frame
    public void ReduceHealth(float Amount)
    {
        health -= Amount;
    }

    public void IncreaseHealth(float Amount)
    {
        health += Amount;
    }
}
