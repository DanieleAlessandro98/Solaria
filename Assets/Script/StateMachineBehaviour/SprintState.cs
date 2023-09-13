using System;
using UnityEngine;

public class SprintState : StateMachineBehaviour
{
    public string Name;
    public bool Continuous;
    public bool Active;
    public bool KeepAction;
    public Func<bool> Continue;

    private float _enterTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enterTime = Time.time;
        animator.SetBool("isSprinting", true);
        Active = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1 && !Continuous)
        {
            Exit(animator, stateInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exit(animator, stateInfo);
    }

    private void Exit(Animator animator, AnimatorStateInfo stateInfo)
    {
        if (!Active || Time.time - _enterTime < stateInfo.length) return;

        Active = false;
        animator.SetBool("isSprinting", false);

        if (Continue == null)
        {
            animator.SetBool("isSprinting", KeepAction);
        }
        else if (Continue != null)
        {
            if (!Continue())
            {
                animator.SetBool("isSprinting", KeepAction);
            }

            Continue = null;
        }
    }
}