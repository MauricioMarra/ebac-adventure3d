using BossStates;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _vSpeed = 0f;
    [SerializeField] private float _runSpeedFactor = 1f;
    [SerializeField] private GameObject _trail;
    [SerializeField] private ParticleSystem _dustPs;

    [SerializeField] private Animator _animator;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;

    private CharacterController _characterController;
    private HealthBase _healthBase;
    private bool _isDead = false;
    private int _damageMultiplier = 1;


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


        if (SaveManager.instance.WasGameLoaded())
        {
            SaveManager.instance.UpdateGameInfo();

            var lastCheckpoint = GameManager.instance.GetLastCheckpointPosition();
            if (lastCheckpoint != Vector3.zero)
            {
                this.transform.position = lastCheckpoint - new Vector3(-5,0,0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded)
        {
            var p = _dustPs.main;
            p.simulationSpeed = 1;
        }

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


        if (Input.GetKeyDown(KeyCode.L) && !_isDead)
            RestoreHealth();
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var p = _dustPs.main;
                p.simulationSpeed = 0;
                _vSpeed = _jumpForce;
                _animator.SetTrigger("Jump");
                
                SfxPool.instance.Play(SoundManager.SoundType.Jump);
            }
        }

        _vSpeed -= _gravity * Time.deltaTime;
        _vSpeed = Mathf.Clamp(_vSpeed, _gravity * -1, _jumpForce * 2);
    }

    public void TakeDamage(float damage)
    {
        _healthBase.TakeDamage(damage);
        PostProcessManager.instance.FlashVignette();
        CameraShake.instance.ShakeCamera();
    }

    public void TakeDamage(float damage, Vector3 hitDirection)
    {
        
    }

    public void OnDeath()
    {
        _animator.SetTrigger("Death");
        _characterController.enabled = false;
        _isDead = true;

        Invoke(nameof(Revive), 3f);
    }

    private void Revive()
    {
        _animator.SetTrigger("Revive");
        _isDead = false;

        var newPosition = GameManager.instance.GetLastCheckpointPosition();
        if (newPosition != Vector3.zero)
        {
            newPosition = new Vector3(newPosition.x - 5, newPosition.y, newPosition.z);
            this.transform.position = newPosition;
        }

        _characterController.enabled = true;

        _healthBase.Revive();
        UIManager.instance.UpdatePlayerHealth(_healthBase.GetMaxHealth() ,_healthBase.GetCurrentHealth());
    }

    public void OnDamage()
    {
        UIManager.instance.UpdatePlayerHealth(_healthBase.GetMaxHealth(), _healthBase.GetCurrentHealth());
    }

    public bool IsPlayerDead()
    {
        return _isDead;
    }

    private void RestoreHealth()
    {
        var item = ItemManager.instance.GetItemByType(ItemType.LifePack);

        if (item != null && _healthBase.GetCurrentHealth() < _healthBase.GetMaxHealth())
        {
            _healthBase.RestoreHealth();
            ItemManager.instance.RemoveItemByType(ItemType.LifePack);
            UIManager.instance.UpdatePlayerHealth(0);
        }
    }

    public void ChangeDamageMultiplier(int damage)
    {
        _damageMultiplier = damage;
    }

    public int GetDamageMultiplier()
    {
        return _damageMultiplier;
    }

    public void SetPlayerRunSpeedBoost(int speed)
    {
        this._speed += speed;
    }

    public void SetTrailsOn(bool trailsOn)
    {
        _trail.SetActive(trailsOn);
    }
}
