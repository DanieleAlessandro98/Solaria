using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //TODO: Ottimizzare collisioni di hit/attacco

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private PlayerController m_PlayerController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
                Attack();
        }
    }

    private void Attack()
    {
        if (!IsAttacking() && !m_PlayerController.IsMoving() && m_PlayerController.IsGrounded())
            m_Animator.SetTrigger("Attack");
    }

    private bool IsAttacking()
    {
        return m_Animator.GetBool("isAttacking");
    }
}
