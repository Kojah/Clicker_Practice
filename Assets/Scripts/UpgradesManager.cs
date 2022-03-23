using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public MainController  mainController;
    public GameData data;

    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void Start()
    {
        data = mainController.data;
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
    }

    public BigDouble Cost () => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, data.clickUpgradeLevel);
}
