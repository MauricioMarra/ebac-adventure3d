using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float _currentLife;
    [SerializeField] private float _maxLife;

    private AnimationBase _animationBase;

    // Start is called before the first frame update
    void Start()
    {
        _currentLife = _maxLife;
        _animationBase = GetComponent<AnimationBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(5f);
    }

    private void Kill()
    {
        Destroy(gameObject, 3f);
        _animationBase.PlayAnimationByType(AnimationType.Death);
    }

    public void TakeDamage(float damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
            Kill();
    }
}
