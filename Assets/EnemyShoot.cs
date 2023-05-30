using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase
{
    [SerializeField] private GunBase _gun;
    [SerializeField] private Player _player;
    [SerializeField] private bool _startShooting;

    protected override void Start()
    {
        base.Start();

        _player = GameObject.Find("PlayerContainer").GetComponent<Player>();

        transform.LookAt(_player.transform);

        if (_startShooting)
            ShootPlayer();
    }

    private void Update()
    {
        transform.LookAt(_player.transform);
    }

    private void ShootPlayer()
    {
        if (_startShooting)
            _gun.StartShoting();
    }
}
