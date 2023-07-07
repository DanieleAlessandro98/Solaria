using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //TODO: Ottimizzazioni varie possibili:
    // -- gameObject.CompareTag("Character") uso di interfacce per identificare il gameobject
    // -- IsAttacking() ereditato in qualche modo sia da enemy sia da player
    // --  character.EnemyHit(); e enemy.PlayerHit(); generalizzare

    //TODO: Mettere nomi statici di queste animazioni da qualche parte (ad esempio anche per "State" e "ChargeAttack2H" in EnemyController)
    private const string ENEMY_ATTACK_ANIMATION = "ChargeAttack2H";

    [SerializeField]
    private Animator m_Animator;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundChecker") || other.CompareTag("Ground"))
            return;

        if (gameObject.CompareTag("Character"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy)
            {
                if (IsAttacking())
                    enemy.PlayerHit(GetComponent<PlayerController>().GetDamage());
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            //TODO: invece di PlayerController implementare una abstract class/interface
            PlayerController character = other.GetComponent<PlayerController>();
            if (character)
            {
                AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

                if (IsAttacking() && (stateInfo.IsName(ENEMY_ATTACK_ANIMATION) && stateInfo.normalizedTime < 1f))
                    character.EnemyHit(GetComponent<EnemyController>().GetDamage());
            }
        }
    }

    private bool IsAttacking()
    {
        return m_Animator.GetBool("isAttacking");
    }
}
