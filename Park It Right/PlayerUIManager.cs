using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public GameObject wonMenu;
    [SerializeField] public GameObject loseMenu;

    public GameObject pauseFirstButton;
    public GameObject wonFirstButton;
    public GameObject loseFirstButton;

    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        wonMenu.gameObject.SetActive(false);
        loseMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            pauseMenu.gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
    }
}
