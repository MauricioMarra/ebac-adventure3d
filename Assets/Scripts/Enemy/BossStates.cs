using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossStates
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);

            boss = (BossBase)obj[0];
        }

    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.message = "Debug from BossBaseInit.";
            base.OnStateEnter(obj);

            Debug.Log(obj[0].ToString());
        }
    }

    public class BossStateIdle : BossStateBase 
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter(obj);
            
            boss.StopAllCoroutines();
        }

        public void EndAttack()
        {
            boss.SwitchState(Boss.BossStates.Init);
        }
    }

    public class BossStateRefresh : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.message = "Debug from BossBasseRefresh";
            base.OnStateEnter(obj);

            boss.Refresh(ResumeAttack);
        }

        private void ResumeAttack()
        {
            boss.SwitchState(Boss.BossStates.Attack);
        }
    }

    public class BossStateAttack : BossStateBase 
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.message = "Debug from BossBaseAttack.";
            base.OnStateEnter(obj);

            boss.Attack(EndAttack);
        }

        public void EndAttack()
        {
            boss.SwitchState(Boss.BossStates.Refresh);
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.message = "Debug from BossBaseDeath.";
            base.OnStateEnter(obj);

            boss.StopAllCoroutines();
        }
    }
}
