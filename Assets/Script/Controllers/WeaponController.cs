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

        Entity entity1 = GetComponent<Entity>();
        Entity entity2 = other.GetComponent<Entity>();

        if (entity1 && entity2)
        {
            if (entity1.IsPlayer() && !entity2.IsPlayer())
            {
                if (entity1.IsAttacking())
                    entity2.RecvHit(GetComponent<Entity>().GetDamage());
            }
            else if (!entity1.IsPlayer() && entity2.IsPlayer())
            {
                AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

                if (entity1.IsAttacking())
                    entity2.RecvHit(GetComponent<Entity>().GetDamage());
            }
        }
    }
}
