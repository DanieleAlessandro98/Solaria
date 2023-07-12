using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerController player = otherCollider.GetComponent<PlayerController>();
        if (player)
            player.SaveLastCheckPointPosition();
    }
}
