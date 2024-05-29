using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WonMenu : MonoBehaviour
{
    PlayerUIManager playerUIManager;
    [SerializeField] private AudioSource winChime;
    [SerializeField] private AudioClip winChimeClip;

    private void OnEnable()
    {
        playerUIManager = FindObjectOfType<PlayerUIManager>();

        Time.timeScale = 0;
        AudioListener.pause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playerUIManager.wonFirstButton);

        winChime.ignoreListenerPause = true;
        winChime.PlayOneShot(winChimeClip);
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
