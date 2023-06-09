using BossStates;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Boss
{
    public enum BossStates
    {
        Init,
        Idle,
        Refresh,
        Move,
        Attack,
        Death
    }

    public class BossBase : MonoBehaviour, IDamagable
    {
        [SerializeField] GameObject _arcProjectile;
        [SerializeField] GameObject _projectileSpawnPoint;
        [SerializeField] GameObject _player;

        [Header("Shoot Attributes")]
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private int _maxAttacks = 3;

        private StateMachine<BossStates> _bossStates;

        private HealthBase _healthBase;
        private AnimationBase _animationBase;




        // Start is called before the first frame update
        void Start()
        {
            _bossStates = new StateMachine<BossStates>();

            _bossStates.Init();
            _bossStates.RegisterState(BossStates.Init, new BossStateInit());
            _bossStates.RegisterState(BossStates.Attack, new BossStateAttack());
            _bossStates.RegisterState(BossStates.Idle, new BossStateIdle());
            _bossStates.RegisterState(BossStates.Refresh, new BossStateRefresh());

            _healthBase = GetComponent<HealthBase>();
            _healthBase.OnKill += OnBossDeath;

            _animationBase = GetComponent<AnimationBase>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SwitchState(BossStates state)
        {
            _bossStates.SwitchState(state, this);
        }


        #region DEBUG
        [NaughtyAttributes.Button]
        public void SwitchInit()
        {
            _bossStates.SwitchState(BossStates.Init, this);
        }

        [NaughtyAttributes.Button]
        public void SwitchAttack()
        {
            _bossStates.SwitchState(BossStates.Attack, this);
        }

        [NaughtyAttributes.Button]
        public void SwitchIdle()
        {
            _bossStates.SwitchState(BossStates.Idle, this);
        }
        #endregion


        public void Attack(Action callback = null)
        {
            StartCoroutine(StartAttacking(callback));
        }

        public void Refresh(Action callback = null)
        {
            StartCoroutine(StartRefreshing(callback));
        }

        public IEnumerator StartAttacking(Action callback = null)
        {
            var _attackCount = 1;

            while (_attackCount <= _maxAttacks)
            {
                _attackCount++;
                transform.DOScale(transform.localScale * 1.2f, .2f).SetLoops(2, LoopType.Yoyo);
                SpawnArcProjectile();
                yield return new WaitForSeconds(_attackDelay);
            }

            callback?.Invoke();
        }

        public IEnumerator StartRefreshing(Action callback = null)
        {
            var count = 0f;
            while (count < 1.5f)
            {
                count += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            callback?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (_healthBase != null)
                _healthBase.TakeDamage(damage);
        }

        public void TakeDamage(float damage, Vector3 hitDirection)
        {
            
        }

        public void OnBossDeath()
        {
            Destroy(gameObject, 3f);

            _animationBase.PlayAnimationByType(AnimationType.Death);
        }

        private void SpawnArcProjectile()
        {
            var p = Instantiate(_arcProjectile, _projectileSpawnPoint.transform.position, Quaternion.identity);
            p.GetComponent<ProjectileArc>().SetTarget(_player);
            p.GetComponent<ProjectileArc>().ShootProjectile();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
                SwitchAttack();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
                SwitchIdle();
        }
    }
}
