using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;

    public PlayerData(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }
}
