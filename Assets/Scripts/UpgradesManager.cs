using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

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
        clickUpgrade.LevelText.text = $"{MainController.Instance.data.clickUpgradeLevel}/10";
        clickUpgrade.CostText.text = Cost().ToString("F2");
        clickUpgrade.BuyText.text = "Buy\nx1";
    }

    public BigDouble Cost () => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, MainController.Instance.data.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        if(MainController.Instance.data.money >= Cost())
        {
            MainController.Instance.data.money -= Cost();
            MainController.Instance.data.clickUpgradeLevel += 1;
        }

        UpdateVentureUI();
    }
}
