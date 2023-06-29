using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDialogue : MonoBehaviour
{
	[SerializeField]
	protected Collider2D m_Collider2D;

	[SerializeField]
	private EDialogName m_DialogName;

	public void OnTriggerEnter2D(Collider2D other)
	{
		//TODO: invece di PlayerController implementare una abstract class/interface
		PlayerController character = other.GetComponent<PlayerController>();
		if (character)
			StartDialog();
	}

	public void OnCollisionEnter2D(Collision2D collision2D)
	{
		PlayerController character = collision2D.collider.GetComponent<PlayerController>();
		if (character)
			StartDialog();
	}

	public void StartDialog()
	{
		DialogManager.Singleton.StartDialog(m_DialogName);
		m_Collider2D.enabled = false;

		Destroy(gameObject);
	}
}
