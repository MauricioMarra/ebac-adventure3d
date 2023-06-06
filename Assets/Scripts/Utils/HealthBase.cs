using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    private Flash _flashComponent;

    public Action OnKill;

    private void Awake()
    {
        _flashComponent = GetComponent<Flash>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_flashComponent != null)
            _flashComponent.FlashObject();

        if (_currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        OnKill.Invoke();
    }
}
