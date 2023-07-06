using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpSpeedUp : PowerUpBase
{
    [SerializeField] float _duration;

    [SerializeField] UnityEvent OnPickUp;
    [SerializeField] UnityEvent OnComplete;

    private string _playerTag = "Player";

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(_playerTag))
        {
            OnPickUp.Invoke();

            StartCoroutine(SpeedUpCoroutine());
        }

    }

    public IEnumerator SpeedUpCoroutine()
    {
        yield return new WaitForSeconds(_duration);

        OnComplete.Invoke();

        Destroy(this.gameObject);
    }
}
