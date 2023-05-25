using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [SerializeField] private GunBase gun;

    protected override void Init()
    {
        base.Init();

        inputActions.Gameplay.Shoot.performed += x => StartShooting();
        inputActions.Gameplay.Shoot.canceled += x => StopShooting();
    }

    void StartShooting()
    {
        gun.StartShoting();
    }

    void StopShooting()
    {
        gun.StopShooting();
    }
}
