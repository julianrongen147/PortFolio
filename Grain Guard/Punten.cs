using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punten : MonoBehaviour
{
    [SerializeField] private int startingAmount;
    public int currentAmount;

    private void Start()
    {
        currentAmount = startingAmount;
    }

    private void Update()
    {
        if (currentAmount < 0)
        {
            currentAmount = 0;
        }
    }

    public void GainGold(int amount)
    {
        currentAmount += amount;
    }
}
