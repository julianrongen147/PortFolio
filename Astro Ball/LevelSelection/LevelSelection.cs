using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] LevelButtons;

    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                LevelButtons[i].interactable = false;
            }
        }
    }
}
