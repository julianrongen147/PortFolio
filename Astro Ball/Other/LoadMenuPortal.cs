using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuPortal : MonoBehaviour
{
    private void Update()
    {
        SceneManager.LoadScene(5);
    }
}
