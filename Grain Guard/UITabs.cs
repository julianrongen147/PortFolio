using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabs : MonoBehaviour
{
    [SerializeField] private GameObject attackUI;
    [SerializeField] private GameObject cropsUI;

    private bool attackUIIsOn;
    private bool canOpenAttackUI;

    private void Start()
    {
        attackUI.gameObject.SetActive(true);
        cropsUI.gameObject.SetActive(false);
        canOpenAttackUI = false;
    }

    public void ShowAttackUI()
    {
        if (canOpenAttackUI)
        {
            cropsUI.gameObject.SetActive(false);
            attackUI.gameObject.SetActive(true);
        }
    }

    public void ShowCropsUI()
    {
        attackUI.gameObject.SetActive(false);
        cropsUI.gameObject.SetActive(true);
        canOpenAttackUI = true;
    }


}
