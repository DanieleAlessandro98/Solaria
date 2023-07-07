using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int currentHealth;
    private int maxHealth;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public float CalcCurrentHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public static string GetHealthFormatting(int currentHealth, int maxHealth)
    {
        return currentHealth + "/" + maxHealth + " HP";
    }
}
