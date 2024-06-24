using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellBuilding : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI sellAmountText;
    [SerializeField] public GameObject UpgradeUI;
    [SerializeField] public GameObject sellButton;
    [SerializeField] public GameObject upgradeButton;
    private BuildingXP buildingXP;


    private void Start()
    {
        UpgradeUI.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    public void SetBuildingXP(BuildingXP building)
    {
        buildingXP = building;
        int sellAmount = buildingXP.GetSellAmount();
        sellAmountText.text = $"Sell for: {sellAmount}";
        UpgradeUI.gameObject.SetActive(true);
        sellButton.SetActive(true);

        if (buildingXP.level > 1)
        {
            sellButton.SetActive(false);
            sellAmountText.text = $"";
        }
    }

    public void ActivateSellFunction()
    {
        if (buildingXP != null && buildingXP.level == 1)
        {
            buildingXP.SellStructure();
        }
    }
}

