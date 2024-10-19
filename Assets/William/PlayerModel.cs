using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    public event EventHandler healthChange;

    [SerializeField]
    private float health;

    public void SetHealth(float amount)
    {
        health = amount;
        healthChange?.Invoke(this, EventArgs.Empty);
    }

    public void AddHealth(float amount)
    {
        health += amount;
        healthChange?.Invoke(this, EventArgs.Empty);
    }

    public void ReduceHealth(float amount)
    {
        health -= amount;
        healthChange?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealth()
    {
        return health;
    }
}
