using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase
{
    [SerializeField] private GunBase _gun;
    [SerializeField] private Player _player;
    [SerializeField] private bool _startShooting;

    private bool _isShooting = false;
    private bool _isPlayerInRange = false;

    protected override void Start()
    {
        base.Start();

        _player = GameObject.Find("PlayerContainer").GetComponent<Player>();

        transform.LookAt(_player.transform);

        //if (_startShooting)
        //    ShootPlayer();
    }

    private void Update()
    {
        if (!_isDead && _player != null && !_player.IsPlayerDead())
            transform.LookAt(_player.transform);

        if (_isDead || _player.IsPlayerDead() || !_isPlayerInRange)
        {
            _gun.StopShooting();
            _isShooting = false;
        }

        if (!_isDead && !_player.IsPlayerDead() && !_isShooting && _isPlayerInRange)
        {
            ShootPlayer();
            _isShooting = true;
        }
    }

    private void ShootPlayer()
    {
       _gun.StartShoting();
        _isShooting = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isPlayerInRange = false;
    }
}
