using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    private static UpgradesManager instance;
    public static UpgradesManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        productionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2 };
        productionUpgradesBasePower = new BigDouble[] { 1, 2, 10 };

        for (int i = 0; i < MainController.Instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < MainController.Instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradesPrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        UpdateVentureUI("click");
        UpdateVentureUI("production");

        Helpers.UpgradeCheck(MainController.Instance.data.clickUpgradeLevel, 3);
    }

    public void Update()
    {
        
    }

    public void UpdateVentureUI(string type, int upgradeID = -1)
    {

        switch (type)
        {
            case "click":
                if (upgradeID == -1)
                {
                    for (int i = 0; i < clickUpgrades.Count; i++)
                    {
                        UpdateUI(clickUpgrades, i, type);
                    }
                }
                else
                {
                    UpdateUI(clickUpgrades, upgradeID, type);
                }
                break;
            case "production":
                if (upgradeID == -1)
                {
                    for (int i = 0; i < productionUpgrades.Count; i++)
                    {
                        UpdateUI(productionUpgrades, i, type);
                    }
                }
                else
                {
                    UpdateUI(productionUpgrades, upgradeID, type);
                }
                break;
        }
    }

    private void UpdateUI(List<Upgrades> upgrades, int id, string type)
    {
        //TODO RE-ENABLE THIS WHEN MAKING THE ACTUAL APP AFTER TUTORIALS
        //upgrades[id].LevelText.text = $"{MainController.Instance.data.clickUpgradeLevel}/10";
        upgrades[id].CostText.text = $"{UpgradeCost(type, id):F2}";
        upgrades[id].BuyText.text = "Buy\nx1";
    }

    public BigDouble UpgradeCost(string type, int upgradeID) 
    { 
        switch(type)
        {
            case "click":
                return clickUpgradeBaseCost[upgradeID] * BigDouble.Pow(clickUpgradeCostMult[upgradeID], (BigDouble)MainController.Instance.data.clickUpgradeLevel[upgradeID]);
            case "production":
                return productionUpgradeBaseCost[upgradeID] * BigDouble.Pow(productionUpgradeCostMult[upgradeID], (BigDouble)MainController.Instance.data.clickUpgradeLevel[upgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int upgradeID)
    {

        switch (type)
        {
            case "click":
                Buy(MainController.Instance.data.clickUpgradeLevel, upgradeID, type);
                break;
            case "production":
                Buy(MainController.Instance.data.productionUpgradeLevel, upgradeID, type);
                break;
        }
        
    }

    private void Buy(List<int> upgradeLevels, int upgradeID, string type)
    {
        if (MainController.Instance.data.money >= UpgradeCost(type, upgradeID))
        {
            MainController.Instance.data.money -= UpgradeCost(type, upgradeID);
            upgradeLevels[upgradeID] += 1;
        }

        UpdateVentureUI(type, upgradeID);
    }
}
