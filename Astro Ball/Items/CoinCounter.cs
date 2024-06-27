using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;

    [HideInInspector] public int CurrentCoins = 0;
    [SerializeField] private TMP_Text _CoinText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _CoinText.text = "Coins: " + CurrentCoins.ToString();
    }

    public void IncreaseCoins(int amount)
    {
        CurrentCoins += amount;
        _CoinText.text = "Coins: " + CurrentCoins.ToString();
    }
}
