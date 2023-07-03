using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerController player = otherCollider.GetComponent<PlayerController>();
        if (player)
            Kill(player);
    }


    private void Kill(PlayerController target)
	{
		target.Die();
	}
}
