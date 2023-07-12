using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player player = otherCollider.GetComponent<Player>();
        if (player)
            player.SaveLastCheckPointPosition();
    }
}
