using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;
    private int coins;
    private SerializableVector2 lastCheckPointPosition;
    private Health health;

    public PlayerData()
    {
        this.level = GameManager.FIRST_LEVEL;
        this.coins = GameManager.FIRST_COIN;
        lastCheckPointPosition = new SerializableVector2(Vector2.zero);
        health = InitHealth();
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCoins()
    {
        return coins;
    }

    public Vector2 GetLastCheckPointPosition()
    {
        return lastCheckPointPosition.ToVector2();
    }

    public Health GetHealth()
    {
        return health;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
    }

    public void SetLastCheckPointPosition(Vector2 lastCheckPointPosition)
    {
        this.lastCheckPointPosition = new SerializableVector2(lastCheckPointPosition);
    }

    public void SetHealth(Health health)
    {
        this.health = health;
    }

    public Health InitHealth()
    {
        return new Health(-1);
    }
}
