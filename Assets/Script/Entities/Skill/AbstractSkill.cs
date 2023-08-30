using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    protected ESkillName name;
    protected float cooldownTimer;
    protected bool isSkillEnable;

    [SerializeField]
    protected float cooldownTime;

    void Start()
    {
        name = ESkillName.NONE;
        cooldownTimer = 0f;
        isSkillEnable = true;
    }
    protected void StartCooldown()
    {
        SetUsedSkillState(true);

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);

        SetUsedSkillState(false);
    }

    public abstract ESkillName GetName();
    public abstract bool CanUseSkill();
    public abstract void UseSkill();
    public abstract bool IsSkillKeyDown();
    public abstract void SetUnlockedSkillState(bool unlocked);
    public abstract void SetUsedSkillState(bool used);
}
