using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalActivate : MonoBehaviour
{
    public bool PortalIsActivated;
    private GemCounter _gemCounter;
    [SerializeField] private GameObject _Portal;

    private void Start()
    {
        _gemCounter = FindObjectOfType<GemCounter>();
        PortalIsActivated = false;
        _Portal.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_gemCounter.CurrentGems == _gemCounter.MaxGems) // if all the gems in the game are picked up activate the portal
        {
            PortalIsActivated = true;
            _Portal.gameObject.SetActive(true);
        }
        else
        {
            PortalIsActivated = false;
        }

    }
}
