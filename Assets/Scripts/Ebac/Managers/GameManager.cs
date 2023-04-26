using Ebac.Core.Singleton;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StateMachine<GameManagerStates> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<GameManagerStates>();
        stateMachine.Init();

        stateMachine.RegisterState(GameManagerStates.Intro, new GameManagerIntroState());
        stateMachine.RegisterState(GameManagerStates.Win, new StateBase());
        stateMachine.RegisterState(GameManagerStates.Lose, new StateBase());
        stateMachine.RegisterState(GameManagerStates.Gameplay, new StateBase());
        stateMachine.RegisterState(GameManagerStates.Paused, new StateBase());

        stateMachine.SwitchState(GameManagerStates.Intro);
    }


    #region DEBUG

    #if UNITY_EDITOR
    [Button]
    public void StartGame()
    {
        stateMachine.SwitchState(GameManagerStates.Gameplay);
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
