using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    protected enum WeaponType
    {
        Projectile, Raycast, Melee
    }

    [SerializeField]
    protected GameObject _projectile;

    [SerializeField]
    protected WeaponType _weaponType = WeaponType.Raycast;
    
    public void Shoot()
    {
        Debug.Log("ba");
        if (!this.isActiveAndEnabled)
            return;
        Debug.Log("b");
        if (_weaponType == WeaponType.Projectile)
        {
            ProjectileShoot();
        }
        else if (_weaponType == WeaponType.Raycast)
        {
            RaycastShoot();
        }
        else if (_weaponType == WeaponType.Melee)
        {
            MeleeShoot();
        }
    }

    protected virtual void RaycastShoot()
    {
        
    }

    protected virtual void ProjectileShoot()
    {

    }

    protected virtual void MeleeShoot()
    {

    }

}
