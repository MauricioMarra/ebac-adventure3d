using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpPowerful : PowerUpBase
{
    [SerializeField] float _duration;

    [SerializeField] UnityEvent OnPickUp;
    [SerializeField] UnityEvent OnComplete;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        OnPickUp.Invoke();

        StartCoroutine(PowerfulShotCoroutine());
    }

    public IEnumerator PowerfulShotCoroutine()
    {
        yield return new WaitForSeconds(_duration);

        OnComplete.Invoke();

        Destroy(this.gameObject);
    }
}
