using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {
    
    enum WeaponType
    {
        Projectile, Raycast, Melee
    }

    GameObject _projectile;

    [SerializeField] WeaponType _weaponType = WeaponType.Raycast;
    
    public void Shoot()
    {
        if (this.isActiveAndEnabled)
            return;

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
