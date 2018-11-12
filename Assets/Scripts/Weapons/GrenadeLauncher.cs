using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : GunBase {

    float _launchSpeed = 10f;

	void Start () {
        _weaponType = WeaponType.Projectile;
	}

    protected override void ProjectileShoot()
    {
        GameObject proj = Instantiate(_projectile);
        proj.GetComponent<Rigidbody>().velocity = transform.right * _launchSpeed;
        proj.transform.position = transform.position;

    }

}
