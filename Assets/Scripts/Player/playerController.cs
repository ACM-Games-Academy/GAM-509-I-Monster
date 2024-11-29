using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField] private float startingHealth;


    public float Health
    { get { return playerModel.Health; } }

    private playerModel playerModel;
    private EventHandler playerDeath;

    private void Start()
    {
        playerModel = new playerModel();

        playerModel.Health = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        playerModel.Health -= damage;
        if (playerModel.Health <= 0)
        {
            Debug.Log("Game Over");
            OnPlayerDeath();
        }
    }

    private void OnPlayerDeath()
    {
        playerDeath.Invoke(this, EventArgs.Empty);
    }
}
