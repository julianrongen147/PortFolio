using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        gameObject.SetActive(false);
    }

    public void SettingsMenu()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
