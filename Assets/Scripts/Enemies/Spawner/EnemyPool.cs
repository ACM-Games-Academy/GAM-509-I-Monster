using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;
    private List<GameObject> pooledEnemies = new List<GameObject>();
    private int amountToPool = 5;

    [SerializeField] private GameObject enemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++) 
        { 
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pooledEnemies.Add(enemy);
        }


    }

    // Update is called once per frame
    public GameObject GetPooledEnemy()
    {
        for(int i = 0;i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }

        return null;
    }
}
