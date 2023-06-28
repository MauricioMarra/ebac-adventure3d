using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBase : MonoBehaviour, IDamagable
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private float _destroyDelay;
    [SerializeField] private int _vibration;

    private HealthBase _health;

    private void OnValidate()
    {
        if (_health == null) _health = this.gameObject.GetComponent<HealthBase>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnValidate();

        _health.OnKill += Kill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button("Shake")]
    private void ShakeObject()
    {
        transform.DOShakeScale(_duration, _strength, _vibration);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        ShakeObject();
    }

    public void TakeDamage(float damage, Vector3 hitDirection)
    {
        _health.TakeDamage(damage);
        ShakeObject();
    }

    public void Kill()
    {
        Destroy(gameObject, _destroyDelay);
    }
}
