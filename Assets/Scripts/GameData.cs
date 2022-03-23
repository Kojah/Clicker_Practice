using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class GameData 
{
    public BigDouble money;

    public List<BigDouble> clickUpgradeLevel;

    public GameData()
    {
        money = 0;

        clickUpgradeLevel = Helpers.CreateList<BigDouble>(3);
    }
}
