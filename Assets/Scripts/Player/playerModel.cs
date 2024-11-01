using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModel : MonoBehaviour
{
    float Health = 100;

    public float GetHealth()
    {
        return Health;
    }

    // Update is called once per frame
    public void ReduceHealth(float Amount)
    {
        Health -= Amount;
    }
}
