using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour
{
    Punten punten;

    [SerializeField] private int buildingCost;

    private Button button;

    private bool buttonPress;

    GridSystem gridSystem;

    private void Start()
    {
        punten = FindObjectOfType<Punten>();
        button = GetComponent<Button>();
        gridSystem = FindObjectOfType<GridSystem>();
    }

    private void Update()
    {
        if (punten.currentAmount < buildingCost && !gridSystem.buildingButtonPrevent)
        {
            button.interactable = false;
        }
        else if (punten.currentAmount >= buildingCost && gridSystem.buildingButtonPrevent)
        {
            button.interactable = true;
        }

        if (gridSystem.buildingButtonPrevent && punten)
        {
            button.interactable = false;
        }
        else if(!gridSystem.buildingButtonPrevent && punten.currentAmount >= buildingCost)
        {
            button.interactable = true;
        }
    }
}
