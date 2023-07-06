using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //TODO: Ottimizzazioni varie possibili:
    // -- gameObject.CompareTag("Character") uso di interfacce per identificare il gameobject
    // -- IsAttacking() ereditato in qualche modo sia da enemy sia da player
    // --  character.EnemyHit(); e enemy.PlayerHit(); generalizzare
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
                    enemy.PlayerHit();
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            //TODO: invece di PlayerController implementare una abstract class/interface
            PlayerController character = other.GetComponent<PlayerController>();
            if (character)
            {
                if (IsAttacking())
                    character.EnemyHit();
            }
        }
    }

    private bool IsAttacking()
    {
        return m_Animator.GetBool("isAttacking");
    }
}
