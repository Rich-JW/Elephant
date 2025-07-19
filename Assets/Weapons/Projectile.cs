

using UnityEngine;



[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

  
    float vectorLength, distanceFromOrigin;
    [SerializeField] ShotData shotData;

    float timer = 0;

    Rigidbody rb;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        timer = 0;
    }
    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        timer = 0;
    }

    private void OnDisable()
    {
       ResetProjectile();
    }

    public void SetProjectile(ShotData _shotData)
    {
        shotData.ResetShotData();
        shotData = _shotData;
    }

 
    void KillProjectile()
    {

        if (shotData.ProjectileKey == null) return;
        
     

        PoolManager.Instance.Return<Projectile>(shotData.ProjectileKey, this);
        timer = 0;

        shotData.ResetShotData();

    }

    

    void PlayHit(Vector3 point, Vector3 normal)
    {

        if (shotData.ProjectileKey == null) return;
        if (shotData.HitEffectKey == null) return;

        HitEffect hitFX = PoolManager.Instance.Get<HitEffect>(shotData.HitEffectKey);

        if (hitFX != null)
        {
            if (normal != Vector3.zero)
            {
                hitFX.transform.position = point + (normal * 0.1f);
                hitFX.transform.rotation = Quaternion.LookRotation(normal);
                hitFX.SetHitEffect(1f, shotData.HitEffectKey);
            }
            else
            {
                return;
            }
        }
    }
    public void ResetProjectile()
    {
       
    }

    private void FixedUpdate()
    {
      
    }

   

    private void Update()
    {

        // Destroy and return to pool after x seconds if no hit detected

        timer += Time.deltaTime;

        if (timer > shotData.Lifetime)
        {
            Debug.Log("Timeout projectile");
            KillProjectile();
        }


        // Measure intended distance and destroy at end of range - replaces traditional collision detection 

         vectorLength = Vector3.Distance(shotData.Point, shotData.Origin);

 
         distanceFromOrigin = Vector3.Distance(shotData.Origin, transform.position); 

        if (distanceFromOrigin >= (vectorLength - (vectorLength * 0.2f)))
        {
            Debug.Log("'Distance' destroy projectile");
            PlayHit(shotData.Point, shotData.Normal);
            KillProjectile();
        }

        // Apply forward movement

        if (rb != null)
        {
            rb.MovePosition(transform.position + shotData.Trajectory * shotData.Speed * Time.deltaTime);
        }
    }

}
