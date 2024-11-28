using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private playerModel playerModel;

    private void Start()
    {
        playerModel = new playerModel();
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        playerModel.ReduceHealth(damage);
        if (playerModel.GetHealth() <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
