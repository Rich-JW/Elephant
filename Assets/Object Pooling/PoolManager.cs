using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PoolItem
{
    [SerializeField] string key;
    [SerializeField] GameObject item;
    [SerializeField] int poolSize;
    [SerializeField] bool instantiateNewIfPoolEmpty;
    public string Key { get { return key; } }
    public GameObject Item { get { return item; } }
    public int PoolSize { get { return poolSize; } }
    public bool InstantiateNewIfPoolEmpty { get { return instantiateNewIfPoolEmpty; } }
}
public class PoolManager : Singleton<PoolManager>   
{

    [SerializeField] List<PoolItem> poolItems = new List<PoolItem>();
    Dictionary<string, Pool<MonoBehaviour>> pools = new Dictionary<string, Pool<MonoBehaviour>>();


    private void Awake()
    {
        base.Awake();
       
    }
    void Start()
    {
        InitializePools();
    }

    void InitializePools()
    {

        Debug.Log("Initializing pools");

        foreach (var poolItem in poolItems)
        {
            Debug.Log("Creating:" + poolItem.Key + " pool");

            // Create game object for each pool

            GameObject itemParent = new GameObject(poolItem.Key);
            itemParent.transform.SetParent(transform, false);
            itemParent.transform.localPosition = Vector3.zero;
            // Create pools

            Pool<MonoBehaviour> pool = new Pool<MonoBehaviour>(poolItem.Item.GetComponent<MonoBehaviour>(), poolItem.PoolSize, itemParent.transform, poolItem.InstantiateNewIfPoolEmpty);

            Debug.Log("Created:" + pool);

            pools.Add(poolItem.Key, pool);
        }
    }

    public T Get<T>(string key) where T : MonoBehaviour
    {
        

        if (pools.TryGetValue(key, out Pool<MonoBehaviour> pool))
        {
            Debug.Log("Pool found:" + key );
            return pool.Get() as T;
        }

        Debug.Log("No pool found for key: " + key);
        return null;

    }

    public void Return<T>(string key, T obj) where T : MonoBehaviour
    {
        if (pools.TryGetValue(key, out Pool<MonoBehaviour> pool))
        {
            pool.Return(obj);
        }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
