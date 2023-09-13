using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : AbstractSkill
{
    [SerializeField]
    private GameObject buttonObject;

    [SerializeField]
    private Button button;

    [SerializeField]
    PlayerController player;

    public override ESkillName GetName()
    {
        return ESkillName.SKILL2;
    }

    public override bool CanUseSkill()
    {
        return buttonObject.activeSelf && isSkillEnable && !SkillManager.Singleton.IsUsingOtherSkills(GetName());
    }

    public override void UseSkill()
    {
        if (CanUseSkill())
        {
            player.SprintAnimation();
            StartCooldown();
        }
    }

    public override bool IsSkillKeyDown()
    {
        return Input.GetKeyDown(KeyCode.F2);
    }

    public override void SetUnlockedSkillState(bool unlocked)
    {
        isSkillEnable = unlocked;
        buttonObject.SetActive(unlocked);
    }

    public override void SetUsedSkillState(bool used)
    {
        isSkillEnable = !used;
        button.interactable = !used;
    }

    public override bool IsUsingSkill()
    {
        return player.IsSprinting();
    }
}
