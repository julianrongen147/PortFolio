using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingXP : MonoBehaviour
{
    [SerializeField] private string towerName;
    [SerializeField] public int level = 1;
    [SerializeField] private int currentXP;
    [SerializeField] private int baseCost;
    [SerializeField] private int baseUpgradeXPCost;
    [SerializeField] private int maxLevel;
    [SerializeField] private GameObject xpBarPrefab;

    private bool maxXPLock = false;
    private int upgradeXPCost;

    //[SerializeField] private GameObject upgradeButton;
    private Button buttonUpgrade;

    private Slider xpBar;
    private GameObject instantiatedXPBar;

    private TextMeshProUGUI xpText;

    Punten punten;
    TowerShooting towerShoot;
    GameObject canvas;
    SellBuilding sellBuilding;
    TowerHealth towerHealth;

    Notifications notifications;

    [SerializeField] private GameObject[] towerPrefabs;

    private bool canInteract = false;
    private GameObject xpArrow;

    private float timer;
    [SerializeField] private float XPGainInterval;
    [SerializeField] private int amountOfXpPerInterval;

    private void Start()
    {
        punten = FindObjectOfType<Punten>();
        towerShoot = FindObjectOfType<TowerShooting>();
        notifications = FindObjectOfType<Notifications>();
        sellBuilding = FindObjectOfType<SellBuilding>();

        towerHealth = GetComponent<TowerHealth>();
        canvas = GameObject.Find("CanvasDynamicUI");

        xpArrow = transform.Find("XPArrow")?.gameObject;
        xpArrow.SetActive(false);

        if (xpArrow == null)
        {
            Debug.LogError("XPArrow child object not found.");
        }
        else
        {
            xpArrow.SetActive(false);
        }

        StartCoroutine(EnableInteractionAfterDelay(0.5f));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > XPGainInterval)
        {
            GainXP(amountOfXpPerInterval);
            timer = 0;
        }


        if (level == maxLevel)
        {
            currentXP = upgradeXPCost;
        }

        if (xpBar != null)
        {
            UpdateXPBar();
            UpdateXPText();
        }
        if (buttonUpgrade == null)
        {
            return;
        }
        if (!xpBar && sellBuilding.upgradeButton.activeInHierarchy)
        {
            CanUpgrade();
        }

        if (level == maxLevel && xpBar != null)
        {
            buttonUpgrade.enabled = false;
        }
    }

    public int GetSellAmount()
    {
        return baseCost * level / 4;
    }

    public void SellStructure()
    {
        if (punten != null)
        {
            int sellAmount = GetSellAmount();
            punten.currentAmount += sellAmount;

            HideUpgradeMenu();

            if (towerHealth != null)
            {
                towerHealth.PublicKill();
            }
            else
            {
                Debug.LogError("TowerHealth component not found.");
            }
        }
        else
        {
            Debug.LogError("Punten is not initialized.");
        }
    }

    private void UpdateXPBar()
    {
        if (xpBar != null)
        {
            if (level < maxLevel)
            {
                int maxXP = baseUpgradeXPCost * level;
                float progress = (float)currentXP / maxXP;
                xpBar.value = progress;
            }
            else
            {
                xpBar.maxValue = 1;
                xpBar.value = 1;
            }
        }
    }

    private void UpdateXPText()
    {
        if (level < maxLevel)
        {
            int remainingXP = baseUpgradeXPCost * level - currentXP;
            xpText.text = $"EXP TO UPGRADE: {remainingXP}";
        }
        else
        {
            xpText.text = $"MAX LEVEL";
        }
    }

    private IEnumerator DelayShowUpgradeMenu()
    {
        yield return new WaitForEndOfFrame();
        ShowUpgradeMenu();
    }

    private IEnumerator EnableInteractionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canInteract = true;
    }

    public void OnTowerClicked()
    {
        if (!canInteract)
        {
            return;
        }

        if (xpBar != null)
        {
            HideUpgradeMenu();
        }
        else
        {
            StartCoroutine(DelayShowUpgradeMenu());
        }
    }

    private void ShowUpgradeMenu()
    {
        // Set up UI elements for this specific tower instance
        sellBuilding.SetBuildingXP(this);

        instantiatedXPBar = Instantiate(xpBarPrefab, canvas.transform);
        xpBar = instantiatedXPBar.GetComponent<Slider>();
        xpText = xpBar.GetComponentInChildren<TextMeshProUGUI>();

        // Instantiate the upgradeButton and parent it under UpgradeUI
        GameObject instantiatedUpgradeButton = Instantiate(sellBuilding.upgradeButton, sellBuilding.UpgradeUI.transform);
        buttonUpgrade = instantiatedUpgradeButton.GetComponent<Button>();

        // Set up listeners specifically for this tower instance
        buttonUpgrade.onClick.AddListener(Upgrade);

        // Adjust UI elements position or scale as needed for this tower
        xpBar.transform.localScale *= 0.01f;
        xpText.transform.localScale *= 1f;

        Vector3 sliderPosition = transform.position + new Vector3(0f, 1.5f, 0f);
        xpBar.transform.position = sliderPosition;

        sellBuilding.sellButton.GetComponent<Button>().onClick.AddListener(sellBuilding.ActivateSellFunction);
    }

    public void HideUpgradeMenu()
    {
        if (sellBuilding.upgradeButton != null)
        {
            if (instantiatedXPBar != null)
            {
                sellBuilding.UpgradeUI.gameObject.SetActive(false);
                Destroy(instantiatedXPBar);
                Destroy(buttonUpgrade.gameObject); // Destroy the instantiated upgradeButton
                xpBar = null;
            }
        }
    }

    private bool CanUpgrade()
    {
        if (level < maxLevel)
        {
            int upgradeCost = baseCost * level;
            upgradeXPCost = baseUpgradeXPCost * level;
            return punten != null && punten.currentAmount >= upgradeCost && currentXP >= upgradeXPCost;
        }
        return false;
    }

    public void Upgrade()
    {
        if (CanUpgrade())
        {
            int upgradeCost = baseCost * level;
            if (punten.currentAmount >= upgradeCost && currentXP >= upgradeXPCost)
            {
                notifications.closeUpgradeNotify();
                maxXPLock = false;
                level++;
                currentXP = 0;
                punten.currentAmount -= upgradeCost;
                Debug.Log($"{towerName} upgraded to level {level}");
                HideUpgradeMenu();
                ChangeTowerLevelPrefab();
                
                //towerShoot.UpgradeTower();
            }
            else
            {
                notifications.BrokeError();
            }
        }
    }

    public void GainXP(int xp)
    {
        int upgradeXPCost = baseUpgradeXPCost * level;
        if (currentXP + xp <= upgradeXPCost)
        {
            currentXP += xp;
        }
        else
        {
            xpArrow.SetActive(true);
            currentXP = upgradeXPCost;
            if (maxXPLock == false)
            {
                notifications.UpgradeNotify();
            }
            maxXPLock = true;
        }
    }

    public void ChangeTowerLevelPrefab()
    {
        if (level >= 1 && level <= towerPrefabs.Length)
        {
            GameObject newTowerPrefab = towerPrefabs[level - 1];

            if (newTowerPrefab != null)
            {
                GameObject newTower = Instantiate(newTowerPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                xpArrow.SetActive(false);
            }
            else
            {
                Debug.LogError($"Prefab for Tower_{towerName}_Level{level} not found!");
            }
        }
        else
        {
            Debug.LogError($"No prefab found for Tower_{towerName} at level {level}.");
        }
    }
}
