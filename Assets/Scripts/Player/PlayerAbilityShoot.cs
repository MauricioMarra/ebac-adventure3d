using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [SerializeField] private GunBase _gun;
    [SerializeField] private Transform _gunPosition;

    [SerializeField] private List<GunBase> _gunList = new List<GunBase>();

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputActions.Gameplay.Shoot.performed += x => StartShooting();
        inputActions.Gameplay.Shoot.canceled += x => StopShooting();

        inputActions.Gameplay.Weapon1.performed += x => SwapWeapon1();
        inputActions.Gameplay.Weapon2.performed += x => SwapWeapon2();
    }

    void CreateGun()
    {
        _currentGun = Instantiate(_gun, _gunPosition);
    }

    void CreateGun(GunBase gun)
    {
        _currentGun = Instantiate(gun, _gunPosition);
    }

    void StartShooting()
    {
        _currentGun.StartShoting();

        SfxPool.instance.Play(SoundManager.SoundType.Shoot);
    }

    void StopShooting()
    {
        _currentGun.StopShooting();
    }

    void SwapWeapon1()
    {
        CreateGun(_gunList[0]);
    }

    void SwapWeapon2()
    {
        CreateGun(_gunList[1]);
    }
}
