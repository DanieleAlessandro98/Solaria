using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager m_Singleton;

    [SerializeField]
    private List<AbstractSkill> m_Skills;

    public static SkillManager Singleton
    {
        get
        {
            return m_Singleton;
        }
    }

    void Awake()
    {
        if (m_Singleton)
        {
            Destroy(gameObject);
            return;
        }

        m_Singleton = this;
    }

    void Start()
    {
        if (m_Skills == null || m_Skills.Count < 1)
            return;

        foreach (AbstractSkill skill in m_Skills)
        {
            bool unlocked = GameDataManager.Singleton.IsSkillUnlocked(skill.GetName());
            skill.SetUnlockedSkillState(unlocked);
        }
    }

    void Update()
    {
        if (m_Skills == null || m_Skills.Count < 1)
            return;

        foreach (AbstractSkill skill in m_Skills)
        {
            if (skill.IsSkillKeyDown() && skill.CanUseSkill())
                skill.UseSkill();
        }
    }

    public void UnlockSkill(ESkillName skillName)
    {
        foreach (AbstractSkill skill in m_Skills)
        {
            if (skill.GetName() == skillName)
            {
                GameDataManager.Singleton.UnlockSkill(skillName);
                skill.SetUnlockedSkillState(true);
            }
        }
    }

}
