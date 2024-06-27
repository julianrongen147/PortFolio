using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrashBox : MonoBehaviour
{
    [SerializeField] private AudioSource _DestroyBoxEffect1;
    [SerializeField] private AudioSource _DestroyBoxEffect2;
    private bool EffectStart;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < 0.01)
        {
            EffectStart = true;
        }
        else
        {
            EffectStart = false;
        }

        if (EffectStart)
        {
                _DestroyBoxEffect1.Play();
        }

        Destroy(gameObject, 5f);
    }
}


