 
using UnityEngine;
 

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] Weapon currentWeapon;

 

    private void Start()
    {

        InitializeWeapons();

       SetWeapon(weapons[0]);

 

        
    }

    void InitializeWeapons()
    {
        weapons = GetComponentsInChildren<Weapon>();    
    }
    public void UpdateWeapon()
    {
        if (weapons != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (weapons.Length > 0)
                {
                   if (weapons[0] != null) SetWeapon(weapons[0]);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (weapons.Length > 1)
                {
                    if (weapons[1] != null) SetWeapon(weapons[1]);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (weapons.Length > 2)
                {
                    if (weapons[2] != null) SetWeapon(weapons[2]);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (weapons.Length > 3)
                {
                    if (weapons[3] != null) SetWeapon(weapons[3]);
                }
            }

            
        }

        
       if (InputManager.Instance.IsPressed("Shoot_Key")) BeginShoot();
       else if (InputManager.Instance.IsPressed("Shoot_Gamepad")) BeginShoot();


        currentWeapon?.UpdateWeaponBob();


    }

    public void BeginShoot( )
    {
  
        Debug.Log("Shooy");
        currentWeapon?.BeginShoot();
    }

    public void EndShoot()
    {
      //  currentWeapon?.EndShoot();
    }

    void SetWeapon(Weapon weaponToSelect)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == weaponToSelect)
            {
                currentWeapon = weapons[i];
                weapons[i].SetMeshActive(true);
            }
            else
            {
                weapons[i].SetMeshActive(false);
            }
        }
    }
}
