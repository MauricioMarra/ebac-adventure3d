using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : EnemyBase
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 5f;

    private float _minDistance = 5f;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_isDead || _player == null) return;

        var distance = Vector3.Distance(_player.transform.position, this.transform.position);

        if (distance < _minDistance)
            return;

        this.transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime);
        this.transform.LookAt(_player.transform);
    }
}
