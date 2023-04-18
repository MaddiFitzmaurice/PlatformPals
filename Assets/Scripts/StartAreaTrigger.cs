using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            _gameManager.HandleStartArea(1, true);
        }
        else if (other.CompareTag("Player2"))
        {
            _gameManager.HandleStartArea(2, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            _gameManager.HandleStartArea(1, false);
        }
        else if (other.CompareTag("Player2"))
        {
            _gameManager.HandleStartArea(2, false);
        }
    }
}
