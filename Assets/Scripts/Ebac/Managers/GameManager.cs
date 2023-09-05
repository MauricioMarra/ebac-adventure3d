using Ebac.Core.Singleton;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StateMachine<GameManagerStates> stateMachine;

    [Header("Checkpoint")]
    [SerializeField] private int _lastCheckpoint;
    [SerializeField] private Vector3 _lastCheckpointPosition;
    [SerializeField] private GameObject _gameNotification;
    [SerializeField] private TextMeshProUGUI _textNotification;

    [Header("Available Chekpoints")]
    [SerializeField] private List<CheckpointBase> _checkpointList;

    [SerializeField] private GameObject _pauseMenu;
    private bool _isPauseMenuActive = false;

    private float _notificationTimer = 3f;
    private float _counter;
    private float _startTime;

    protected Inputs _inputActions;

    private void Start()
    {
        stateMachine = new StateMachine<GameManagerStates>();
        stateMachine.Init();

        stateMachine.RegisterState(GameManagerStates.Intro, new GameManagerIntroState());
        stateMachine.RegisterState(GameManagerStates.Win, new StateBase());
        stateMachine.RegisterState(GameManagerStates.Lose, new StateBase());
        stateMachine.RegisterState(GameManagerStates.Gameplay, new GameManagerRunningState());
        stateMachine.RegisterState(GameManagerStates.Paused, new GameManagerPauseState());

        stateMachine.SwitchState(GameManagerStates.Intro);

        ItemManager.instance.items.ForEach(x => x.scriptableObjects.Reset());

        _inputActions = new Inputs();
        _inputActions.Enable();

        SoundManager.instance.PlayMusicByType(SoundManager.SoundType.Type01);
    }

    private void Update()
    {
        _inputActions.Gameplay.PauseGame.performed += x => PauseGame();

        if (_counter < _notificationTimer)
        {
            var elapsedTime = Time.realtimeSinceStartup;
            _counter = elapsedTime - _startTime;

            if (_counter > _notificationTimer)
                _gameNotification.SetActive(false);
        }
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

    public void RegisterCheckpoint(CheckpointBase checkpoint)
    {
        _checkpointList.Add(checkpoint);
    }

    public CheckpointBase FindCheckpointById(int id)
    {
        var checkpoint = _checkpointList.Find(x => x.GetKey() == id);

        return checkpoint;
    }

    public void PauseGame()
    {
        var current = stateMachine.GetCurrentState();

        if (current.GetType() == typeof(GameManagerPauseState)) return;

        stateMachine.SwitchState(GameManagerStates.Paused);
        TogglePauseMenu();
    }

    public void ResumeGame()
    {
        stateMachine.SwitchState(GameManagerStates.Gameplay);
        TogglePauseMenu();
    }

    public void QuitGame()
    {
        TogglePauseMenu(false);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void TogglePauseMenu()
    {
        _isPauseMenuActive = !_isPauseMenuActive;
        _pauseMenu.SetActive(_isPauseMenuActive);
    }

    public void TogglePauseMenu(bool active)
    {
        _pauseMenu.SetActive(active);
    }

    public void SaveGame()
    {
        SaveManager.instance.SaveGame();
    }

    public void ShowNotification(string text)
    {
        _counter = 0f;
        _startTime = Time.realtimeSinceStartup;
        _gameNotification.SetActive(true);
        _textNotification.text = text;
    }
}

public enum GameManagerStates
{
    Intro,
    Win,
    Lose,
    Gameplay,
    Paused
}
