using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BreakInfinity;

public class MainController : MonoBehaviour
{
    private static MainController instance;
    public static MainController Instance {  get { return instance; } }

    public GameData data;
    [SerializeField] TMP_Text moneyText;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }


    private void Start()
    {
        data = new GameData();
        UpgradesManager.Instance.StartUpgradeManager();
    }

    private void Update()
    {
        moneyText.text = "$ " + data.money.ToString("F2");
    }

    public void AddMoney()
    {
        data.money += ClickPower();
    }

    public BigDouble ClickPower() 
    {
        BigDouble total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.Instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }

}
