using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;
    private int coins;

    public PlayerData(int level, int coins)
    {
        this.level = level;
        this.coins = coins;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
    }
}
