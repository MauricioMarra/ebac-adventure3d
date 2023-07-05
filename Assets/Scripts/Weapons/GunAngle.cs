using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAngle : GunLimit
{
    [SerializeField] private int _maxBulletsPerShot = 4;
    private float _bulletAngle = 15f;

    protected override void Shoot()
    {
        var multiplier = 1;

        for (int i = 1;  i <= _maxBulletsPerShot; i++)
        {
            var p = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            p.ChangeDamageMultiplier(player.GetComponent<Player>().GetDamageMultiplier());

            p.transform.rotation = Quaternion.Euler(Vector3.zero);

            var side = i % 2 == 0 ? 1 : -1;
            
            p.transform.eulerAngles = Vector3.zero + (Vector3.up * multiplier * side * _bulletAngle + transform.rotation.eulerAngles);

            p.transform.SetParent(null);

            if (i % 2 == 0)
                multiplier++;
        }
    }
}
