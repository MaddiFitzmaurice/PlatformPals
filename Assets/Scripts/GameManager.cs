using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Flags 
    private bool _isPlayer1StartArea;
    private bool _isPlayer2StartArea;
    private bool _isPlayer1EndArea;
    private bool _isPlayer2EndArea;

    [SerializeField] private PlatformManager _platformManager;
    [SerializeField] private int _nextLevel;

    public void Start()
    {
        _platformManager.HandleLevelEditorMode(true);
    }

    // Handle when players enter or leave the start area
    public void HandleAreaTrigger(int player, bool isInArea, AreaTrigger.AreaType type)
    {
        if (type == AreaTrigger.AreaType.StartArea)
        {
            if (player == 1)
            {
                _isPlayer1StartArea = isInArea;
            }
            else if (player == 2)
            {
                _isPlayer2StartArea = isInArea;
            }
        }
        else if (type == AreaTrigger.AreaType.EndArea)
        {
            if (player == 1)
            {
                _isPlayer1EndArea = isInArea;
            }
            else if (player == 2)
            {
                _isPlayer2EndArea = isInArea;
            }
        }

        // If both players are in starting area, turn on level editor
        if (_isPlayer1StartArea && _isPlayer2StartArea)
        {
            _platformManager.HandleLevelEditorMode(true);
        }
        // If one player is not in starting area, turn off level editor
        else if (!_isPlayer1StartArea || !_isPlayer2StartArea)
        {
            _platformManager.HandleLevelEditorMode(false);
        }

        // If both players are in the end area
        if (_isPlayer1EndArea && _isPlayer2EndArea)
        {
            ChangeLevel();
        }
    }

    void ChangeLevel()
    {
        if (_nextLevel <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_nextLevel);
        }
        else
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
    }

}
