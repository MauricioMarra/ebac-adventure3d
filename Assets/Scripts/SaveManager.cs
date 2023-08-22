using Ebac.Core.Singleton;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    [SerializeField] private Player _playerReference;

    private string _saveFilePath;

    private bool _wasGameLoaded = false;

    public override void Awake()
    {
        base.Awake();

        _saveFilePath = Application.dataPath + $"/SaveGame/Save";

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _saveSetup = new SaveSetup();
    }

    private void Update()
    {
        if (_playerReference == null)
        {
            _playerReference = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        }


    }

    public void CreateSaveFile()
    {
        if (File.Exists(_saveFilePath)) return;
    }

    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveSetup.playerName = "Mauricio";
        _saveSetup.lastCheckPointID = GameManager.instance.GetLastCheckPointID();
        _saveSetup.lastCheckpoint = GameManager.instance.GetLastCheckpointPosition();
        _saveSetup.coins = ItemManager.instance.GetItemByType(ItemType.Coin)?.scriptableObjects?.value ?? 0;
        _saveSetup.lifePacks = ItemManager.instance.GetItemByType(ItemType.LifePack)?.scriptableObjects?.value ?? 0;
        _saveSetup.health = _playerReference.GetComponent<HealthBase>().GetCurrentHealth();

        var json = JsonUtility.ToJson( _saveSetup );

        File.WriteAllText( _saveFilePath, json );

        GameManager.instance.ShowNotification("Game saved!");
    }

    [NaughtyAttributes.Button]
    public void LoadGame()
    {
        var saveFile = File.ReadAllText(_saveFilePath);

        _saveSetup = JsonUtility.FromJson<SaveSetup>(saveFile);

        _wasGameLoaded = true;
    }

    public void UpdateGameInfo()
    {
        for (int i = 0; i < _saveSetup.coins; i++)
        {
            ItemManager.instance?.AddItemByType(ItemType.Coin);
        }

        for (int i = 0; i < _saveSetup.lifePacks; i++)
        {
            ItemManager.instance?.AddItemByType(ItemType.LifePack);
        }

        GameManager.instance.SaveCheckpoint(_saveSetup.lastCheckPointID, _saveSetup.lastCheckpoint);
        ActivateVisitedCheckpoints();

        if (_playerReference == null)
            _playerReference = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();

        _playerReference?.GetComponent<HealthBase>()?.SetCurrentHealth(_saveSetup.health);
        UIManager.instance?.UpdatePlayerHealth(_playerReference.GetComponent<HealthBase>().GetMaxHealth(), _saveSetup.health);

        _wasGameLoaded = false;
    }

    public bool WasGameLoaded()
    {
        return _wasGameLoaded;
    }

    private void ActivateVisitedCheckpoints()
    {
        var lastCheckpointID = _saveSetup.lastCheckPointID;

        for (int i = 1; i <= lastCheckpointID; i++)
        {
            var checkPoint = GameManager.instance.FindCheckpointById(i);

            checkPoint?.ActivateCheckpoint();
        }
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastCheckPointID;
    public Vector3 lastCheckpoint;
    public string playerName;
    public int coins;
    public int lifePacks;
    public float health;
}
