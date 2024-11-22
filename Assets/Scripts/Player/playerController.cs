using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private playerModel playerModel;
    public InputActionReference leftGrab;
    public InputActionReference rightGrab;
    public GameObject leftLaser;
    public GameObject rightLaser;

    private void Start()
    {
        playerModel = new playerModel();
    }

    private void Update()
    {
        leftLaser.SetActive(leftGrab.action.ReadValue<float>() > 0.5f);
        rightLaser.SetActive(rightGrab.action.ReadValue<float>() > 0.5f);
    }

    public void TakeDamage(float damage)
    {
        playerModel.ReduceHealth(damage);
        if (playerModel.GetHealth() <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.transform.name == "gun")
        {
            TakeDamage(10);
        }
    }
}
