using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool _IsPaused;

    [SerializeField] private GameObject _PauseMenuUI;

    private void Start()
    {
        _PauseMenuUI.gameObject.SetActive(false);    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _IsPaused = true;
        }

        if (_IsPaused)
        {
            Time.timeScale = 0;
            _PauseMenuUI.gameObject.SetActive(true);

            AudioSource[] audios = FindObjectsOfType<AudioSource>();

            foreach (AudioSource a in audios)
            {
                a.Pause();
            }
        }

        if (!_IsPaused)
        {
            Time.timeScale = 1;
            AudioSource[] audios = FindObjectsOfType<AudioSource>();

            foreach (AudioSource a in audios)
            {
                a.UnPause();
            }

            _PauseMenuUI.gameObject.SetActive(false);
        }
    }

    public void OnContinueClick()
    {
        _IsPaused = false;
        
    }

    public void OnLeaveClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
