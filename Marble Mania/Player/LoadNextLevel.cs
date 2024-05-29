using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public int NextSceneLoad;

    private void Start()
    {
        NextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                Debug.Log("You win the game!");
            }
            else
            {
                SceneManager.LoadScene(NextSceneLoad);

                //setting the int
                if (NextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", NextSceneLoad);
                }
            }
        }
    }

}
