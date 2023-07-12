using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private const int DAMAGE_TO_PLAYER = 1;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerController player = otherCollider.GetComponent<PlayerController>();
        if (player)
            player.EnemyHit(GetComponent<Spike>().GetDamage(), true);
    }

    public int GetDamage()
    {
        return DAMAGE_TO_PLAYER;
    }
}
