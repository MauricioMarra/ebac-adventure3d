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

    [Space]
    [Header("Drop Item Config")]
    [SerializeField] GameObject _dropable;
    [SerializeField] GameObject _dropOrigin;
    [SerializeField] float _multiplier;
    [SerializeField] int _quantity;

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
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
        DropItem();
        Destroy(gameObject, _destroyDelay);
    }

    [NaughtyAttributes.Button]
    private void DropItem()
    {
        for (int i = 1; i <= _quantity; i++)
        {
            var obj = Instantiate(_dropable, _dropOrigin.transform.position + Vector3.up * 5, Quaternion.identity);
            obj.transform.Rotate(Vector3.up * 120 * i);
            obj.GetComponent<Rigidbody>()?.AddForce(obj.transform.forward * _multiplier);
        }
    }
}
