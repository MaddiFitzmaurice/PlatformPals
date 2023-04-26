using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _instructions;

    void Start()
    {
        Cursor.visible = true;
        _mainMenu.SetActive(true);
        _instructions.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void InstructionsActive(bool flag)
    {
        _instructions.SetActive(flag);
        _mainMenu.SetActive(!flag);
    }
}
