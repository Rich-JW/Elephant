using UnityEngine;

public class HitEffect : MonoBehaviour
{
    float lifetime, timer;
    string key;

    private void OnDisable()
    {

    }

    public void SetHitEffect(float _lifetime, string _key)
    {
        lifetime = _lifetime;
        key = _key; 
    }

    void ReturnToPool()
    {
       if (key != null) PoolManager.Instance.Return<HitEffect>(key, this);
        timer = 0;
        lifetime = 0;   
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            ReturnToPool();
        }
    }
}
