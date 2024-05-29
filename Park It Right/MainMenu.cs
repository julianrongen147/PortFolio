using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject carMenu;

    Animator animator;

    private bool OpenCarMenu = false;

    float timer;
    bool StartTimer;

    public GameObject mainMenuFirstButton;

    public Button play, tutorial, settings;

    bool test = false;

    public AudioSource closeSlide;
    public AudioClip closeSlideClip;

    bool canCloseMenu = true;

    private void Start()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;

        animator = carMenu.GetComponent<Animator>();
        carMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    private void Update()
    {
        if (!OpenCarMenu && Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.C))
        {
            OpenCarMenu = true;
            test = false;
            carMenu.SetActive(true);
        }

        if (OpenCarMenu)
        {
            CarMenu();
        }


        if (StartTimer)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                carMenu.SetActive(false);
                timer = 0;
                StartTimer = false;
                canCloseMenu = true;
            }
        }

        if (OpenCarMenu)
        {
            play.interactable = false;
            tutorial.interactable = false;
            settings.interactable = false;
        }

        if (!OpenCarMenu && !test)
        {
            play.interactable = true;
            tutorial.interactable = true;
            settings.interactable = true;
            EventSystem.current.SetSelectedGameObject(null);
            test = true;
        }
    }

    private void CarMenu()
    {
        animator.SetBool("Show", true);

        StartCoroutine(CanCloseCarMenu());

    }

    private IEnumerator CanCloseCarMenu()
    {
        yield return new WaitForSeconds(.5f);
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.C) && canCloseMenu)
        {
            if (canCloseMenu)
            {
                closeSlide.PlayOneShot(closeSlideClip);
                canCloseMenu = false;
            }
            StartTimer = true;
            animator.SetBool("Show", false);
            OpenCarMenu = false;
        }
    }

    public void TutorialPlay()
    {

    }

    public void PlayGame()
    {
        //go level selector screen. no scene change
        SceneManager.LoadScene(1);
    }

    public void SettingsGame()
    {

    }
}
