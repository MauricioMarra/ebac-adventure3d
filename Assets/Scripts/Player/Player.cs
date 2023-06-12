using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _vSpeed = 0f;
    [SerializeField] private float _runSpeedFactor = 1f;

    [SerializeField] private Animator _animator;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;

    private CharacterController _characterController;
    private HealthBase _healthBase;
    private bool _isDead = false;


    private void OnValidate()
    {
        if (_healthBase == null)
            _healthBase = GetComponent<HealthBase>();
    }

    private float _runSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        OnValidate();

        UIManager.instance.UpdatePlayerHealth(0);

        _healthBase.OnKill += OnDeath;
        _healthBase.OnDamage += OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_runKey))
        {
            _runSpeed = _runSpeedFactor;
            _animator.speed = _runSpeedFactor;
        }
        else
        {
            _runSpeed = 1;
            _animator.speed = 1;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime, 0);

        var verticallInput = Input.GetAxis("Vertical");
        Vector3 _movementVector = transform.forward * _speed * verticallInput * _runSpeed;

        _animator.SetBool("Run", verticallInput != 0);

        Jump();
        _movementVector.y = _vSpeed;

        _characterController.Move(_movementVector * Time.deltaTime);

        _animator.SetBool("IsGrounded", _characterController.isGrounded);
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = _jumpForce;
                _animator.SetTrigger("Jump");
            }
        }

        _vSpeed -= _gravity * Time.deltaTime;
        _vSpeed = Mathf.Clamp(_vSpeed, _gravity * -1, _jumpForce * 2);
    }

    public void TakeDamage(float damage)
    {
        _healthBase.TakeDamage(damage);
    }

    public void TakeDamage(float damage, Vector3 hitDirection)
    {
        
    }

    public void OnDeath()
    {
        _animator.SetTrigger("Death");
        _characterController.enabled = false;
        _isDead = true;

        Destroy(this.gameObject, 3f);

        Debug.Log("Player is dead.");
    }

    public void OnDamage()
    {
        UIManager.instance.UpdatePlayerHealth(_healthBase.GetMaxHealth(), _healthBase.GetCurrentHealth());
    }

    public bool IsPlayerDead()
    {
        return _isDead;
    }
}
