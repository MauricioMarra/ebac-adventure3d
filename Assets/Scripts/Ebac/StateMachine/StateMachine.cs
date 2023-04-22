using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    private Dictionary<T, StateBase> _statesDictionary;
    private StateBase _currentState;

    public void Init()
    {
         _statesDictionary = new Dictionary<T, StateBase>();
    }

    public void RegisterState(T state)
    {
        _statesDictionary.Add(state, new StateBase());
    }

    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = _statesDictionary[state];

        _currentState.OnStateEnter();
    }

    public StateBase GetCurrentState()
    {
        return _currentState;
    }
}

public enum States
{
    INTRO,
    GAMEPLAY,
    WIN,
    LOSE,
    PAUSE
}
