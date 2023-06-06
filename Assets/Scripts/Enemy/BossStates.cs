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
            boss.SwitchState(Boss.BossStates.Init);
        }
    }
}
