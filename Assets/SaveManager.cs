using Ebac.Core.Singleton;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;

    private void Start()
    {
        _saveSetup = new SaveSetup();
    }

    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveSetup.playerName = "Mauricio";
        _saveSetup.lastCheckpoint = GameManager.instance.GetLastCheckpointPosition();

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
    }
}

[System.Serializable]
public class SaveSetup
{
    public Vector3 lastCheckpoint;
    public string playerName;
}
