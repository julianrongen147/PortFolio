using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public static PlayerExp instance;
    [SerializeField] public Image expBar;
    ScoreManager scoremanager;


    private void Start()
    {
        instance = this;
        expBar = GetComponent<Image>();
        scoremanager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        expBar.fillAmount = (float)scoremanager.Exp / (float)scoremanager.nextLevelUp;
    }
}
