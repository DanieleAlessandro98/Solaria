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
		PlayerController player = other.GetComponent<PlayerController>();
		if (player)
			StartDialog();
	}

	public void OnCollisionEnter2D(Collision2D collision2D)
	{
		PlayerController player = collision2D.collider.GetComponent<PlayerController>();
		if (player)
			StartDialog();
	}

	public void StartDialog()
	{
		DialogManager.Singleton.StartDialog(m_DialogName);
		m_Collider2D.enabled = false;

		Destroy(gameObject);
	}
}
