using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadGround : MonoBehaviour
{
    private const int DAMAGE_TO_PLAYER = 1;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Entity entity = otherCollider.GetComponent<Entity>();
        if (entity && entity.IsPlayer())
            entity.RecvHit(GetComponent<DeadGround>().GetDamage(), true);
    }

    public int GetDamage()
    {
        return DAMAGE_TO_PLAYER;
    }
}
