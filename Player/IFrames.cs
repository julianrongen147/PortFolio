using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFrames : MonoBehaviour
{
    public IFrames iframes;

    Renderer rend;
    Color c;
    public bool IFramesSwitch = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.CompareTag("Bullet"))
        {
            IFramesSwitch = true;
            StartCoroutine("GetInvulnerable");
        }
    }

    IEnumerator GetInvulnerable()
    {
        if (IFramesSwitch)
        {
            Physics.IgnoreLayerCollision(6, 7, true);
            c.r = 0.23f;
            c.g = 0.23f;
            c.b = 0.23f;
            rend.material.color = c;
            yield return new WaitForSeconds(2f);
            Physics.IgnoreLayerCollision(6, 7, false);
            c.r = 1f;
            c.g = 1f;
            c.b = 1f;
            rend.material.color = c;
            IFramesSwitch = false;
        }
    }
}
