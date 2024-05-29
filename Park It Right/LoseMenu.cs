using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoseMenu : MonoBehaviour
{
    PlayerUIManager playerUIManager;
    [SerializeField] private AudioSource loseChime;
    [SerializeField] private AudioClip loseChimeClip;

    private void OnEnable()
    {
        playerUIManager = FindObjectOfType<PlayerUIManager>();

        Time.timeScale = 0;
        AudioListener.pause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playerUIManager.loseFirstButton);

        loseChime.ignoreListenerPause = true;
        loseChime.PlayOneShot(loseChimeClip);
    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
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
