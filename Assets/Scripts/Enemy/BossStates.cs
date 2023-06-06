using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossStates
{
    public class BossStateBase : StateBase
    {

    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] obj)
        {
            base.OnStateEnter();

            Debug.Log(obj[0].ToString());
        }
    }

}
