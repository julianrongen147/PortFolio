using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevels : MonoBehaviour
{
    public void levelSelection(int level)
    {
        if (level == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (level == 1)
        {
            SceneManager.LoadScene(2);
        }
        if (level == 2)
        {
            SceneManager.LoadScene(3);
        }
        if (level == 3)
        {
            SceneManager.LoadScene(4);
        }
    }
}
