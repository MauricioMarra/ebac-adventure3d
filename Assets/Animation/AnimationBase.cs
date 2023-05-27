using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour
{
    [SerializeField] private List<AnimationSetup> _setups;
    [SerializeField] private Animator _animator;

    public void PlayAnimationByType(AnimationType animationType)
    {
        var a = _setups.Find(x => x.type == animationType);
        _animator.SetTrigger(a.triggerName);
    }
}

public enum AnimationType
{
    None,
    Attack,
    Run,
    Death,
    Idle
}

[System.Serializable]
public class AnimationSetup
{
    public AnimationType type;
    public string triggerName;
}