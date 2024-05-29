using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _MainMenuUI;
    [SerializeField] private GameObject _LevelUI;

    private void Start()
    {
        _LevelUI.gameObject.SetActive(false);
        _MainMenuUI.gameObject.SetActive(true);
    }

    public void OnBackClick()
    {
        _MainMenuUI.gameObject.SetActive(true);
        _LevelUI.gameObject.SetActive(false);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnClickPlay()
    {
        Time.timeScale = 1;
        _MainMenuUI.gameObject.SetActive(false);
        _LevelUI.gameObject.SetActive(true);
    }
}
