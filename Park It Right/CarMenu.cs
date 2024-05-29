using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarMenu : MonoBehaviour
{
    public GameObject[] childMenus;
    public Image[] switcherMenus;
    private int currentMenuIndex = 0;
    private int currentMenuIndex2 = 0;
    private float buttonCooldown = 0.1f;
    private float lastButtonTime = 0f;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color activeColor;

    public GameObject carMenuFirstButton;

    public AudioSource slideSound;
    public AudioClip slideClip;

    void Start()
    {
        SetActiveMenu(currentMenuIndex);
        SetActiveSwitcher(currentMenuIndex2);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(carMenuFirstButton);
    }

    void Update()
    {
        if (Time.time - lastButtonTime >= buttonCooldown)
        {
            if (Input.GetButtonDown("L1"))
            {
                ChangeMenu(-1);
                ChangeMenu2(-1);
            }
            else if (Input.GetButtonDown("R1"))
            {
                ChangeMenu(1);
                ChangeMenu2(1);
            }
        }


    }

    void ChangeMenu(int change)
    {
        int newMenuIndex = (currentMenuIndex + change + childMenus.Length) % childMenus.Length;

        newMenuIndex = Mathf.Clamp(newMenuIndex, 0, childMenus.Length - 1);

        if (newMenuIndex != currentMenuIndex)
        {
            SetActiveMenu(newMenuIndex);
            lastButtonTime = Time.time;
        }
    }

    void SetActiveMenu(int index)
    {
        for (int i = 0; i < childMenus.Length; i++)
        {
            childMenus[i].SetActive(false);
        }

        childMenus[index].SetActive(true);

        currentMenuIndex = index;
    }

    void ChangeMenu2(int change)
    {
        int newMenuIndex = (currentMenuIndex2 + change + switcherMenus.Length) % switcherMenus.Length;

        newMenuIndex = Mathf.Clamp(newMenuIndex, 0, switcherMenus.Length - 1);

        if (newMenuIndex != currentMenuIndex2)
        {
            SetActiveSwitcher(newMenuIndex);
            lastButtonTime = Time.time;
        }
    }

    void SetActiveSwitcher(int index)
    {
        for (int i = 0; i < switcherMenus.Length; i++)
        {
            // Reset color for all images to normal color
            Color imageColor = normalColor;
            imageColor.a = 1f; // Ensure alpha is 1 (fully opaque)
            switcherMenus[i].color = imageColor;
        }

        // Set the active menu image color
        Color activeImageColor = activeColor;
        activeImageColor.a = 1f; // Ensure alpha is 1 (fully opaque)
        switcherMenus[index].color = activeImageColor;

        currentMenuIndex2 = index;
    }

    public void PlaySlideSound()
    {
        slideSound.PlayOneShot(slideClip);
    }
}
