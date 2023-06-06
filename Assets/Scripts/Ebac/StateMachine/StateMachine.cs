using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> statesDictionary;
    private StateBase _currentState;

    public void Init()
    {
         statesDictionary = new Dictionary<T, StateBase>();
    }

    public void RegisterState(T stateEnum, StateBase state)
    {
        statesDictionary.Add(stateEnum, state);
    }

    public void SwitchState(T state, params object[] obj)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = statesDictionary[state];

        _currentState.OnStateEnter(obj);
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
