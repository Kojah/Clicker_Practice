using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradeSelected;
    public GameObject ProductionUpgradeSelected;

    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;

    public void SwitchUpgrades(string location)
    {
        UpgradesManager.Instance.clickUpgradesScroll.gameObject.SetActive(false);
        UpgradesManager.Instance.productionUpgradesScroll.gameObject.SetActive(false);

        //ClickUpgradeSelected.SetActive(false);
        //ProductionUpgradeSelected.SetActive(false);

        ClickUpgradesTitleText.color = Color.grey;
        ProductionUpgradesTitleText.color = Color.grey;

        switch (location)
        {
            case "click":
                UpgradesManager.Instance.clickUpgradesScroll.gameObject.SetActive(true);
                //ClickUpgradeSelected.SetActive(true);
                ClickUpgradesTitleText.color = Color.white;
                break;
            case "production":
                UpgradesManager.Instance.productionUpgradesScroll.gameObject.SetActive(true);
                //ProductionUpgradeSelected.SetActive(true);
                ProductionUpgradesTitleText.color = Color.white;
                break;
        }
    }
}
