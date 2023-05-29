using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamagable
{
    [SerializeField] private float _currentLife;
    [SerializeField] private float _maxLife;
    [SerializeField] private ParticleSystem hitParticleSystem;

    private AnimationBase _animationBase;
    private Flash _flashComponent;

    // Start is called before the first frame update
    void Start()
    {
        _currentLife = _maxLife;
        _animationBase = GetComponent<AnimationBase>();
        _flashComponent = GetComponent<Flash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            OnDamage(5f);
    }

    private void Kill()
    {
        Destroy(gameObject, 3f);
        _animationBase.PlayAnimationByType(AnimationType.Death);
    }

    public void OnDamage(float damage)
    {
        if (hitParticleSystem != null)
            hitParticleSystem.Emit(10);

        if (_flashComponent != null)
            _flashComponent.FlashObject();

        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public void TakeDamage(float damage)
    {
        OnDamage(damage);
    }
}
