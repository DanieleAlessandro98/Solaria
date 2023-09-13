using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Health
{
    private int currentHealth;
    private int maxHealth;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void SetCurrentHealth(int damage)
    {
        if (!IsAlive())
            return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public bool IsValid()
    {
        return maxHealth != -1 && currentHealth != -1;
    }

    public static float CalcCurrentHealthPct(int currentHealth, int maxHealth)
    {
        return (float)currentHealth / maxHealth;
    }

    public static string GetHealthFormatting(int currentHealth, int maxHealth)
    {
        return currentHealth + "/" + maxHealth + " HP";
    }
}
