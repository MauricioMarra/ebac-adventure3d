using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveManager : GameManager
{
    private SaveSetup _saveSetup;

    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveSetup = new SaveSetup();

        _saveSetup.playerName = "Mauricio";
        _saveSetup.lastCheckpoint = GameManager.instance.GetLastCheckpointPosition();

        var json = JsonUtility.ToJson( _saveSetup );

        var path = Application.dataPath + $"/SaveGame/Save";

        File.WriteAllText( path, json );
    }
}

public class SaveSetup
{
    public Vector3 lastCheckpoint;
    public string playerName;
}
