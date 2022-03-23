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

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;


    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        for(int i = 0; i < MainController.Instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        UpdateVentureUI();

        Helpers.UpgradeCheck(ref MainController.Instance.data.clickUpgradeLevel, 4);
    }

    public void Update()
    {
        
    }

    public void UpdateVentureUI(int upgradeID = -1)
    {
        if(upgradeID == -1)
        {
            for (int i = 0; i < clickUpgrades.Count; i++)
            {
                UpdateUI(i);
            }
        }
        else
        {
            UpdateUI(upgradeID);
        }
    }

    private void UpdateUI(int id)
    {
        //TODO RE-ENABLE THIS WHEN MAKING THE ACTUAL APP AFTER TUTORIALS
        //clickUpgrades[id].LevelText.text = $"{MainController.Instance.data.clickUpgradeLevel}/10";
        clickUpgrades[id].CostText.text = $"{ClickUpgradeCost(id):F2}";
        clickUpgrades[id].BuyText.text = "Buy\nx1";
    }

    public BigDouble ClickUpgradeCost (int upgradeID) => clickUpgradeBaseCost[upgradeID] * BigDouble.Pow(clickUpgradeCostMult[upgradeID], MainController.Instance.data.clickUpgradeLevel[upgradeID]);

    public void BuyUpgrade(int upgradeID)
    {
        if(MainController.Instance.data.money >= ClickUpgradeCost(upgradeID))
        {
            MainController.Instance.data.money -= ClickUpgradeCost(upgradeID);
            MainController.Instance.data.clickUpgradeLevel[upgradeID] += 1;
        }

        UpdateVentureUI(upgradeID);
    }
}
