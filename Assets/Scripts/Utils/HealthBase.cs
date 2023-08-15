using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private ParticleSystem _hitParticleSystem;

    private Flash _flashComponent;

    public Action OnKill;
    public Action OnDamage;

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

        if (_hitParticleSystem != null)
            _hitParticleSystem.Emit(30);

        OnDamage?.Invoke();

        if (_currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        OnKill?.Invoke();
    }

    public void Revive()
    {
        _currentHealth = _maxHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetCurrentHealth(float health)
    {
        _currentHealth = health;
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
    }
}
