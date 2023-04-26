using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private bool _showFoldOut;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("State Machine");

        if (gameManager.stateMachine == null) return;

        if (gameManager.stateMachine.GetCurrentState() != null)
            EditorGUILayout.LabelField("Current State:", gameManager.stateMachine.GetCurrentState().ToString());

        _showFoldOut = EditorGUILayout.Foldout(_showFoldOut, "Available States");
        if (_showFoldOut)
        {
            for(int i = 0; i < gameManager.stateMachine.statesDictionary.Count; i++) 
            {
                EditorGUILayout.LabelField(string.Format("{0} - {1}", gameManager.stateMachine.statesDictionary.Keys.ToArray()[i], gameManager.stateMachine.statesDictionary.Values.ToArray()[i]));
            }
        }
    }
}
