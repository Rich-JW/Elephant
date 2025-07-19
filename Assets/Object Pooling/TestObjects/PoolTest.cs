
using UnityEngine;

public class PoolTest : MonoBehaviour
{

    [SerializeField] string itemSpawnKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float spawnRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var item = PoolManager.Instance.Get<PoolTestItem>(itemSpawnKey);

            if (item != null) item.transform.position = GetRandomPos();
            else Debug.Log("No item found with name: " + itemSpawnKey);
        }

       
    }

    Vector3 GetRandomPos()
    {
        return new Vector3(0 + Random.Range(-spawnRange, spawnRange), 0 + Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
    }
}
