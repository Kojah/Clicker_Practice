using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using System.Linq;

public class GameData 
{
    public BigDouble money;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public GameData()
    {
        money = 0;

        clickUpgradeLevel = new int[3].ToList();
        productionUpgradeLevel = new int[3].ToList();
    }
}
