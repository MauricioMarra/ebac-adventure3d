using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    [SerializeField] private List<GameObject> _patrolPositions = new List<GameObject>();
    [SerializeField] private float _speed;

    private int nextPoint = 0;

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (_isDead) return;

        if (_patrolPositions.Count > 0)
        {
            if (Vector3.Distance(transform.position, _patrolPositions[nextPoint].transform.position) < 0.1f)
            {
                nextPoint++;
            }

            if (nextPoint ==  _patrolPositions.Count)
                nextPoint = 0;

            if (transform.position != _patrolPositions[nextPoint].transform.position)
                transform.position = Vector3.MoveTowards(transform.position, _patrolPositions[nextPoint].transform.position, _speed * Time.deltaTime);
                //transform.Translate((transform.position - _patrolPositions[nextPoint].transform.position).normalized * Time.deltaTime * _speed);

            transform.LookAt(_patrolPositions[nextPoint].transform.position);
        }
    }
}
