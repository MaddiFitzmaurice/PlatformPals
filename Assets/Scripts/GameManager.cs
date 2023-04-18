using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Flags 
    private bool _isPlayer1StartArea;
    private bool _isPlayer2StartArea;

    [SerializeField] private PlatformManager _platformManager;

    // Handle when players enter or leave the start area
    public void HandleStartArea(int player, bool isInStartArea)
    {
        if (player == 1)
        {
            _isPlayer1StartArea = isInStartArea;
        }
        //else if (player == 2)
        //{
            //_isPlayer2StartArea = isInStartArea;
        //}

        // If both players are in starting area, turn on level editor
        if (_isPlayer1StartArea) //&& _isPlayer2StartArea)
        {
            _platformManager.HandleLevelEditorMode(true);
        }
        // If one player is not in starting area, turn off level editor
        else if (!_isPlayer1StartArea) //|| !_isPlayer2StartArea)
        {
            _platformManager.HandleLevelEditorMode(false);
        }
    }

}
