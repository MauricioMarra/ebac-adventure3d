using BossStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public enum BossStates
    {
        Init,
        Move,
        Death
    }

    public class BossBase : MonoBehaviour, IDamagable
    {
        private StateMachine<BossStates> _bossStates;

        private HealthBase _healthBase;
        private AnimationBase _animationBase;

        // Start is called before the first frame update
        void Start()
        {
            _bossStates = new StateMachine<BossStates>();

            _bossStates.Init();
            _bossStates.RegisterState(BossStates.Init, new BossStateInit());

            _healthBase = GetComponent<HealthBase>();
            _healthBase.OnKill += OnBossDeath;

            _animationBase = GetComponent<AnimationBase>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [NaughtyAttributes.Button]
        public void SwitchInit()
        {
            _bossStates.SwitchState(BossStates.Init, this);
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
    }
}
