using Ebac.Core.Singleton;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    [SerializeField] private Player _playerReference;

    private bool _wasGameLoaded = false;

    public override void Awake()
    {
        base.Awake();

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

    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveSetup.playerName = "Mauricio";
        _saveSetup.lastCheckPointID = GameManager.instance.GetLastCheckPointID();
        _saveSetup.lastCheckpoint = GameManager.instance.GetLastCheckpointPosition();
        _saveSetup.coins = ItemManager.instance.GetItemByType(ItemType.Coin).scriptableObjects.value;
        _saveSetup.lifePacks = ItemManager.instance.GetItemByType(ItemType.LifePack).scriptableObjects.value;
        _saveSetup.health = _playerReference.GetComponent<HealthBase>().GetCurrentHealth();

        var json = JsonUtility.ToJson( _saveSetup );

        var path = Application.dataPath + $"/SaveGame/Save";

        File.WriteAllText( path, json );
    }

    [NaughtyAttributes.Button]
    public void LoadGame()
    {
        var file = Application.dataPath + $"/SaveGame/Save";
        var saveFile = File.ReadAllText(file);

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
