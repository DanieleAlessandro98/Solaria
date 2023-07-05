using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
            Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Punch");
    }
}
