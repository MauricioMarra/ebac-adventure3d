using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : EnemyBase
{
    [SerializeField] private GameObject _player;

    private float _minDistance = 4f;
    private float _speed = .05f;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_isDead) return;

        var distance = Vector3.Distance(_player.transform.position, this.transform.position);

        if (distance < _minDistance)
            return;

        this.transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, _speed);
        this.transform.LookAt(_player.transform);
    }
}
