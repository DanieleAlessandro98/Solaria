using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Weapon"))
            return;

        PlayerInterface entity = otherCollider.GetComponentInChildren<Player>();
        if (entity != null)
            entity.SetCanClimbing(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
            return;

        PlayerInterface entity = collision.GetComponentInChildren<Player>();
        if (entity != null)
            entity.SetCanClimbing(false);
    }
}
