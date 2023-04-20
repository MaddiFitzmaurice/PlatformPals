using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public enum AreaType
    {
        StartArea, EndArea
    }

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AreaType _areaType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            _gameManager.HandleAreaTrigger(1, true, _areaType);
        }
        else if (other.CompareTag("Player2"))
        {
            _gameManager.HandleAreaTrigger(2, true, _areaType);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            _gameManager.HandleAreaTrigger(1, false, _areaType);
        }
        else if (other.CompareTag("Player2"))
        {
            _gameManager.HandleAreaTrigger(2, false, _areaType);
        }
    }
}
