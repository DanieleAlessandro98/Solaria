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
    private Dictionary<ESkillName, bool> skills;

    public PlayerData()
    {
        this.level = GameManager.FIRST_LEVEL;
        this.coins = GameManager.FIRST_COIN;
        lastCheckPointPosition = new SerializableVector2(Vector2.zero);
        health = InitHealth();
        skills = InitSkills();
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

    public Dictionary<ESkillName, bool> GetSkills()
    {
        return skills;
    }

    public bool IsSkillUnlocked(ESkillName skillName)
    {
        return skills.ContainsKey(skillName) && skills[skillName];
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

    public void SetSkills(Dictionary<ESkillName, bool> skills)
    {
        this.skills = skills;
    }

    public void UnlockSkill(ESkillName skillName)
    {
        skills[skillName] = true;
    }

    public Health InitHealth()
    {
        return new Health(-1);
    }

    private Dictionary<ESkillName, bool> InitSkills()
    {
        Dictionary<ESkillName, bool> _skills = new Dictionary<ESkillName, bool>();

        foreach (ESkillName skillName in System.Enum.GetValues(typeof(ESkillName)))
        {
            if (skillName != ESkillName.NONE)
                _skills.Add(skillName, false);
        }

        return _skills;
    }
}
