using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField]
    protected SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    protected Collider2D m_Collider2D;

	public void OnTriggerEnter2D(Collider2D other)
	{
		//TODO: invece di PlayerController implementare una abstract class/interface
		Player player = other.GetComponent<Player>();
		if (player)
		{
			Collect();
			player.SetCoinsText();
		}
	}

	public void OnCollisionEnter2D(Collision2D collision2D)
	{
		Player player = collision2D.collider.GetComponent<Player>();
		if (player)
		{
			Collect();
			player.SetCoinsText();
		}
	}

	public void Collect()
	{
		GameManager.Singleton.CollectCoin();

		m_SpriteRenderer.enabled = false;
		m_Collider2D.enabled = false;

		Destroy(gameObject);
	}

}
