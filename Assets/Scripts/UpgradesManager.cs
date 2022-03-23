using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public MainController  mainController;

    public Upgrades clickUpgrade;


    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void StartUpgradeManager()
    {
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
        UpdateVentureUI();
    }

    public void Update()
    {
        
    }

    public void UpdateVentureUI()
    {
        clickUpgrade.LevelText.text = $"{mainController.data.clickUpgradeLevel}/10";
        clickUpgrade.CostText.text = Cost().ToString("F2");
        clickUpgrade.BuyText.text = "Buy\nx1";
    }

    public BigDouble Cost () => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, mainController.data.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        if(mainController.data.money >= Cost())
        {
            mainController.data.money -= Cost();
            mainController.data.clickUpgradeLevel += 1;
        }

        UpdateVentureUI();
    }
}
