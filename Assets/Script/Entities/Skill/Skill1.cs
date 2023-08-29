using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill1 : AbstractSkill
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private float jumpStrength = 0.1f;

    [SerializeField]
    PlayerController player;

    public override void UseSkill()
    {
        if (CanUseSkill())
        {
            if (player.Jump(jumpStrength))
                StartCooldown();
        }
    }

    public override bool IsSkillKeyDown()
    {
        return Input.GetKeyDown(KeyCode.F1);
    }

    public override void ChangeSkillState(bool enable)
    {
        isSkillEnable = enable;
        button.interactable = enable;
    }
}
