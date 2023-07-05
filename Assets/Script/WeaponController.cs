using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	//TODO: Mettere nomi statici di queste animazioni da qualche parte (ad esempio anche per "State" e "ChargeAttack2H" in EnemyController)
	private const string ATTACK_ANIMATION = "ChargeAttack2H";

	[SerializeField]
	private Animator m_Animator;

	public void OnTriggerEnter2D(Collider2D other)
	{
		//TODO: invece di PlayerController implementare una abstract class/interface
		PlayerController character = other.GetComponent<PlayerController>();
		if (character)
        {
			if (IsAttacking())
				character.EnemyHit();
		}
	}

	private bool IsAttacking()
    {
		AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
		bool isChargeAttack2HRunning = stateInfo.IsName(ATTACK_ANIMATION) && stateInfo.normalizedTime < 1f;

		return isChargeAttack2HRunning;
	}
}
