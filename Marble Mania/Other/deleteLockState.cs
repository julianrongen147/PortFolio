using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteLockState : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
