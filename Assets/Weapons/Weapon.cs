

using UnityEngine;
using WeaponBobbing;



public struct ShotData
{
 

   Quaternion rotation;

   string projectileKey;
   string hitEffectKey;

   Vector3 trajectory;
   Vector3 origin;
   Vector3 point, normal;

   RaycastHit hit;

   float damage;
   float lifetime;
   float speed;


    public Vector3 Point        { get { return point; } }
    public Vector3 Normal       { get { return normal; } }
    public Vector3 Trajectory   { get { return trajectory; } }
    public Vector3 Origin       { get { return origin; } }
    public float Damage         { get { return damage; } }
    public float Lifetime       { get { return lifetime; } }
    public float Speed          { get { return speed; } }
    public string ProjectileKey { get { return projectileKey; } }
    public string HitEffectKey  { get { return hitEffectKey; } }
    public Quaternion Rotation  { get { return rotation; } }

    public void SetShotData(float _speed,  float _lifetime, string _projectileKey, string _hitEffectKey, Vector3 _origin, float _damage)
    {
        ResetShotData();

        speed = _speed;
        lifetime = _lifetime;
        projectileKey = _projectileKey;
        damage = _damage;
        hitEffectKey = _hitEffectKey;
        origin = _origin;

    }

    public void SetHitData(Vector3 _point, Vector3 _normal, Vector3 _trajectory, Quaternion _rotation)
    {
        point = _point;
        normal = _normal;
        trajectory = _trajectory;
        rotation = _rotation;
    }

    public void ResetShotData()
    {
        speed = 0;
        trajectory = Vector3.zero;
        rotation = Quaternion.identity;
        lifetime = 0;
        projectileKey = string.Empty;
        damage = 0;
        hitEffectKey = string.Empty;
        origin = Vector3.zero;
        point = Vector3.zero;
        normal = Vector3.zero;
        trajectory = Vector3.zero;
        rotation = Quaternion.identity; 

    }

}

public enum ShootStyle { Single, Charge, Auto }
public class Weapon : MonoBehaviour
{

    [SerializeField] GameObject mesh;

    [SerializeField] string name;
    [SerializeField] float baseDamage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float fireRate;
    [SerializeField] string projectileKey;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] MuzzleFlash muzzleFlash;
    [SerializeField] float projectileLifetime;
    [SerializeField] LayerMask mask;
    [SerializeField] int projectilesPerShot;
    [SerializeField] float accuracy;
    [SerializeField] string hitEffectKey;
    [SerializeField] double earlyDetonationScale;
    Camera cam;

    [SerializeField] ShotData shotData;

    WeaponBob bob;

    private void Start()
    {
        cam = Camera.main;

        shotData = new ShotData(); 

        bob = GetComponent<WeaponBob>();    
    }
    public void SetMeshActive(bool b)
    {
        mesh.SetActive(b);
    }

   

    public void BeginShoot()
    {

        if (projectileSpawnPoint == null) return;

        // Muzzle flash

        if (muzzleFlash != null) muzzleFlash.PlayMuzzleFlash();

        // Raycast 

        shotData.SetShotData(projectileSpeed, projectileLifetime, projectileKey, hitEffectKey, projectileSpawnPoint.position, baseDamage);

        for (int i = 0; i < projectilesPerShot; i++)
        {

            HandleRaycast();

            Projectile p = PoolManager.Instance.Get<Projectile>(projectileKey);

            if (p == null) return;

            p.transform.position = projectileSpawnPoint.position;

            p.SetProjectile(shotData);

        }
        
    }


     

    Quaternion GetAccuracyOffset()
    {
        Quaternion pitch = Quaternion.identity;
        Quaternion yaw = Quaternion.identity;

        pitch = Quaternion.AngleAxis(Random.Range(-accuracy, accuracy), transform.right);
        yaw = Quaternion.AngleAxis(Random.Range(-accuracy, accuracy), transform.up);

        return pitch * yaw; 
    }

    void HandleRaycast()
    {
        if (cam == null) return;
        if (projectileSpawnPoint == null) return;

        // Generate rotational offset for accuracy variation

        Quaternion accuracyOffset = GetAccuracyOffset();

        // Directional vector

        Vector3 shotDir = Vector3.zero;
        shotDir = accuracyOffset * shotDir;

        Quaternion rot = Quaternion.identity;


        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 1000f, mask))
            {
                Vector3 trajectory =  hit.point - projectileSpawnPoint.transform.position;

                // Ensure projectile faces direction of travel 

                rot = Quaternion.LookRotation(trajectory);

                shotData.SetHitData(hit.point, hit.normal, trajectory.normalized, rot);
        } 
        else
        {

            Vector3 trajectory = cam.transform.forward * 1000f;

            rot = Quaternion.LookRotation(trajectory);

            shotData.SetHitData(projectileSpawnPoint.position + cam.transform.forward * 1000f, new Vector3(0.001f, 0.001f, 0.001f), trajectory.normalized, rot);
        }
    }    

    public void EndShoot()
    {

    }

    private void Update()
    {
       
            Debug.DrawRay(cam.transform.position, cam.transform.forward.normalized * 1000f, Color.green);
         
        
    }

    public void UpdateWeaponBob()
    {
        bob?.UpdateBob();
    }
}
