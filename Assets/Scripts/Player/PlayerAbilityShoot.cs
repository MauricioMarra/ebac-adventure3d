using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [SerializeField] private GunBase _gun;
    [SerializeField] private Transform _gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputActions.Gameplay.Shoot.performed += x => StartShooting();
        inputActions.Gameplay.Shoot.canceled += x => StopShooting();
    }

    void CreateGun()
    {
        _currentGun = Instantiate(_gun, _gunPosition);
    }

    void StartShooting()
    {
        _currentGun.StartShoting();
    }

    void StopShooting()
    {
        _currentGun.StopShooting();
    }
}
