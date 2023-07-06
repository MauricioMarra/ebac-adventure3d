using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpPowerful : PowerUpBase
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

            StartCoroutine(PowerfulShotCoroutine());
        }
    }

    public IEnumerator PowerfulShotCoroutine()
    {
        yield return new WaitForSeconds(_duration);

        OnComplete.Invoke();

        Destroy(this.gameObject);
    }
}
