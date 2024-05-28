using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarBackGround : MonoBehaviour
{

    private void Update()
    {
        if (PlayerHealth.instance.currentPlayerHealth <= 20)
        {
            gameObject.SetActive(false);
        }
        if (PlayerHealth.instance.currentPlayerHealth >= 21)
        {
            gameObject.SetActive(true);
        }

    }
}
