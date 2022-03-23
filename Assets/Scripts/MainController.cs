using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BreakInfinity;

public class MainController : MonoBehaviour
{
    public UpgradesManager upgradesManager;
    public GameData data;
    [SerializeField] TMP_Text moneyText;


    private void Start()
    {
        data = new GameData();
        upgradesManager.StartUpgradeManager();
    }

    private void Update()
    {
        moneyText.text = $"${data.money}";
    }

    public void AddMoney()
    {
        data.money += ClickPower();
    }

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;

}
