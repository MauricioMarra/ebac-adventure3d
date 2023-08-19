using Ebac.Core.Singleton;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StateMachine<GameManagerStates> stateMachine;

    [Header("Checkpoint")]
    [SerializeField] private int _lastCheckpoint;
    private Vector3 _lastCheckpointPosition;

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

        ItemManager.instance.items.ForEach(x => x.scriptableObjects.Reset());
    }

    public void SaveCheckpoint(int key, GameObject checkpoint)
    {
        if (key > _lastCheckpoint)
        {
            _lastCheckpoint = key;
            _lastCheckpointPosition = checkpoint.transform.position;
        }
    }

    public void SaveCheckpoint(int key, Vector3 checkpoint)
    {
        if (key > _lastCheckpoint)
        {
            _lastCheckpoint = key;
            _lastCheckpointPosition = checkpoint;

        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return _lastCheckpointPosition  == null ? Vector3.zero : _lastCheckpointPosition;
    }

    public int GetLastCheckPointID()
    {
        return _lastCheckpoint;
    }

    #region DEBUG

    #if UNITY_EDITOR
    [Button]
    public void StartGame()
    {
        //stateMachine.SwitchState(GameManagerStates.Gameplay);
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
