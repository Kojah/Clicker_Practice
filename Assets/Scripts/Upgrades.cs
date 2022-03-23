using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public Image UpgradeButton;
    public TMP_Text BuyText;
    public TMP_Text CostText;
    public TMP_Text CostSubText;
    public TMP_Text LevelText;

    public void BuyClickUpgrade()
    {
        UpgradesManager.Instance.BuyUpgrade(UpgradeID);
    }
}
