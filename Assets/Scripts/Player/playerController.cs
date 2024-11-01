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

    void OnCollisionEnter(Collision other)
    {
        playerModel.TakeDamage(101);
    }
}
