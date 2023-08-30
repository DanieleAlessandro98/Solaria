using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDialogue : MonoBehaviour
{
	[SerializeField]
	protected Collider2D m_Collider2D;

	[SerializeField]
	private EDialogName m_DialogName;

	[SerializeField]
	private bool unlockSkill = false;

	[SerializeField]
	private ESkillName unlockSkillName = ESkillName.NONE;

	public void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	public void OnCollisionEnter2D(Collision2D collision2D)
	{
		HandleCollision(collision2D.collider);
	}

	private void HandleCollision(Collider2D collider)
    {
		//TODO: invece di PlayerController implementare una abstract class/interface
		PlayerController player = collider.GetComponent<PlayerController>();
		if (player)
		{
			StartDialog();

			if (unlockSkill)
				SkillManager.Singleton.UnlockSkill(unlockSkillName);
		}
	}

	private void StartDialog()
	{
		DialogManager.Singleton.StartDialog(m_DialogName);
		m_Collider2D.enabled = false;

		Destroy(gameObject);
	}
}
