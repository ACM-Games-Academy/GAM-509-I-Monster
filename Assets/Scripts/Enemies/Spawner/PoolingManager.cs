using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    [SerializeField] private GameObject helicopterPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private int helicopterPoolSize = 5;
    [SerializeField] private int tankPoolSize = 5;

    private EnemyPool helicopterPool;
    private EnemyPool tankPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize separate pools
        helicopterPool = CreatePool(helicopterPrefab, helicopterPoolSize);
        tankPool = CreatePool(tankPrefab, tankPoolSize);
    }

    private EnemyPool CreatePool(GameObject prefab, int initialPoolSize)
    {
        GameObject poolObject = new GameObject(prefab.name + " Pool");
        EnemyPool newPool = poolObject.AddComponent<EnemyPool>();
        newPool.Initialize(prefab, initialPoolSize);
        return newPool;
    }

    // Public access to pools
    public EnemyPool GetHelicopterPool() => helicopterPool;
    public EnemyPool GetTankPool() => tankPool;
}
