using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private playerModel playerModel;

    private void Start()
    {
        playerModel = GetComponent<playerModel>();
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
