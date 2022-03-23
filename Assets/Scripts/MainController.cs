using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BreakInfinity;

public class MainController : MonoBehaviour
{
    public GameData data;
    [SerializeField] TMP_Text moneyText;

    private void Start()
    {
        data = new GameData();
        data.money = BigDouble.Pow(1, 55000);
    }

    private void Update()
    {
        moneyText.text = $"${data.money}";
        data.money *= 1000;
        Debug.Log(data.money);
    }

    public void AddMoney()
    {
        data.money += 1;
    }
}
