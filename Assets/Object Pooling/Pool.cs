using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    Queue<T> pool = new Queue<T>();
    T prefab;
    Transform parent;

    bool instantiateNewIfPoolEmpty;
    public Pool(T obj, int size, Transform trans, bool instantiateNewIfPoolEmpty)
    {
        parent = trans;
        prefab = obj;
        this.instantiateNewIfPoolEmpty = instantiateNewIfPoolEmpty;

        for (int i = 0; i < size; i++)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(GameObject.Instantiate(prefab, parent));
        }

    }

    public T Get()
    {

        if (pool.Count == 0)
        {
            if (instantiateNewIfPoolEmpty)
            {
                Debug.Log("No items in pool - instantiating: " + prefab.name);
                pool.Enqueue(GameObject.Instantiate(prefab, parent));
            }
            else
            {
                Debug.Log($"No {prefab} items found in pool!");
                return null;
            }
        }

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
  
}
