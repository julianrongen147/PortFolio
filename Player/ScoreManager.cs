using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUpScreen;
    public static ScoreManager instance;

    public int Exp = 0;

    public int nextLevelUp = 100;

    public GameObject levelUpScreens;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }


    public void AddPoint(int amount)
    {
        Exp += amount;


        if (Exp >= nextLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Exp = 0;
        levelUpScreens = Instantiate(levelUpScreen, GameObject.Find("Canvas").transform);
        Time.timeScale = 0f;
    }
}
