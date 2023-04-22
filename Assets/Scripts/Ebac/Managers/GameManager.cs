using Ebac.Core.Singleton;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private StateMachine<GameManagerStates> _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine<GameManagerStates>();
        _stateMachine.Init();

        _stateMachine.RegisterState(GameManagerStates.Intro);
        _stateMachine.RegisterState(GameManagerStates.Win);
        _stateMachine.RegisterState(GameManagerStates.Lose);
        _stateMachine.RegisterState(GameManagerStates.Gameplay);
        _stateMachine.RegisterState(GameManagerStates.Paused);
    }


    #region DEBUG

#if UNITY_EDITOR
    [Button]
    public void StartGame()
    {
        _stateMachine.SwitchState(GameManagerStates.Gameplay);
    }
    #endif

    #endregion
}

public enum GameManagerStates
{
    Intro,
    Win,
    Lose,
    Gameplay,
    Paused
}
