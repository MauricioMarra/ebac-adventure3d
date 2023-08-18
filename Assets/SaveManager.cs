using Ebac.Core.Singleton;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    [SerializeField] private Player _playerReference;

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
            UpdateGameInfo();
        }
    }

    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveSetup.playerName = "Mauricio";
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

        UpdateGameInfo();
    }

    private void UpdateGameInfo()
    {
        for (int i = 0; i < _saveSetup.coins; i++)
        {
            ItemManager.instance?.AddItemByType(ItemType.Coin);
        }

        for (int i = 0; i < _saveSetup.lifePacks; i++)
        {
            ItemManager.instance?.AddItemByType(ItemType.LifePack);
        }

        _playerReference = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _playerReference?.GetComponent<HealthBase>()?.SetCurrentHealth(_saveSetup.health);
        UIManager.instance?.UpdatePlayerHealth(_playerReference.GetComponent<HealthBase>().GetMaxHealth(), _saveSetup.health);
    }
}

[System.Serializable]
public class SaveSetup
{
    public Vector3 lastCheckpoint;
    public string playerName;
    public int coins;
    public int lifePacks;
    public float health;
}
