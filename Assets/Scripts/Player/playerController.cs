using System;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float startingHealth;


    public float Health
    { get { return playerModel.Health; } }

    private playerModel playerModel;
    public EventHandler playerDeath;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject enemyCounter;
    private int numOfEnemies = 0;
    private GameObject[] enemies;

    private void Start()
    {
        playerModel = new playerModel();

        playerModel.Health = startingHealth;

        numOfEnemies = 0;

        healthBar.GetComponentInChildren<TMP_Text>().text = playerModel.Health.ToString();
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numOfEnemies = enemies.Length;
        enemyCounter.GetComponentInChildren<TMP_Text>().text = numOfEnemies.ToString();
    }

    public void TakeDamage(float damage)
    {
        playerModel.Health -= damage;
        healthBar.GetComponentInChildren<TMP_Text>().text = playerModel.Health.ToString();
        if (playerModel.Health <= 0)
        {
            Debug.Log("Game Over");
            OnPlayerDeath();
        }
    }

    public void IncreaseHealth(float health)
    {
        playerModel.IncreaseHealth(health);
        healthBar.GetComponentInChildren<TMP_Text>().text = playerModel.Health.ToString();
    }

    private void OnPlayerDeath()
    {
        playerDeath?.Invoke(this, EventArgs.Empty);
    }
}
