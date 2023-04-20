using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Flags 
    private bool _isPlayer1InArea;
    private bool _isPlayer2InArea;

    [SerializeField] private PlatformManager _platformManager;
    [SerializeField] private int _nextLevel;

    public void Start()
    {
        _platformManager.HandleLevelEditorMode(true);
    }

    // Handle when players enter or leave the start area
    public void HandleAreaTrigger(int player, bool isInArea, AreaTrigger.AreaType type)
    {
        if (player == 1)
        {
            _isPlayer1InArea = isInArea;
        }
        else if (player == 2)
        {
            _isPlayer2InArea = isInArea;
        }

        // If both players are in starting area, turn on level editor
        if (_isPlayer1InArea && type == AreaTrigger.AreaType.StartArea && _isPlayer2InArea)
        {
            _platformManager.HandleLevelEditorMode(true);
        }
        // If one player is not in starting area, turn off level editor
        else if (!_isPlayer1InArea && type == AreaTrigger.AreaType.StartArea || !_isPlayer2InArea)
        {
            _platformManager.HandleLevelEditorMode(false);
        }

        // If both players are in the end area
        if (_isPlayer1InArea && _isPlayer2InArea && type == AreaTrigger.AreaType.EndArea)
        {
            ChangeLevel();
        }
    }

    void ChangeLevel()
    {
        if (_nextLevel <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_nextLevel - 1);
        }
        else
        {
            EditorApplication.ExitPlaymode();
        }
    }

}
