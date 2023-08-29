using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    protected float cooldownTimer;
    protected bool isSkillEnable;

    [SerializeField]
    protected float cooldownTime;

    void Start()
    {
        cooldownTimer = 0f;
        isSkillEnable = true;
    }

    public virtual bool CanUseSkill()
    {
        return isSkillEnable;
    }

    protected void StartCooldown()
    {
        ChangeSkillState(false);

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);

        ChangeSkillState(true);
    }

    public abstract void UseSkill();
    public abstract bool IsSkillKeyDown();
    public abstract void ChangeSkillState(bool enable);
}
