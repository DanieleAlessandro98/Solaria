using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Entity, PlayerInterface
{
	private readonly float DELAY_ENTITY_DIED = 3f;

	private PlayerController controller;

	[SerializeField]
	private TextMeshProUGUI m_CoinsText;

	[SerializeField]
	private HealthPlayerBoard m_HealthBoard;

	[SerializeField]
	private int m_MaxHealth;

	[SerializeField]
	private int m_Damage;

    private void Awake()
    {
		SetHealthGUI(m_HealthBoard);
	}

	void Start()
	{
		controller = GetComponent<PlayerController>();

		if (GameManager.Singleton.IsValidLastCheckPointPosition())
			controller.MoveToLastCheckpoint();

		if (!GameManager.Singleton.IsValidHealth())
			SetHealth(new Health(m_MaxHealth));
		else
			SetHealth(GameDataManager.Singleton.GetHealth());
		RecvDamage(0);

		SetCoinsText();
	}

	public float GetPositionX()
	{
		return transform.position.x;
	}

    public override void Die(bool isResetPosition)
    {
		GameManager.Singleton.SetDead(true);
		Invoke("EntityDied", isResetPosition ? 0f : DELAY_ENTITY_DIED);
	}

    public override void EntityDied()
    {
		GameManager.Singleton.Dead();
	}

    public override int GetDamage()
    {
		return m_Damage;
	}

    public void SaveLastCheckPointPosition()
    {
		GameDataManager.Singleton.SetLastCheckPointPosition(new Vector2(transform.position.x, transform.position.y));
    }

    public void SetCoinsText()
    {
		m_CoinsText.text = GameDataManager.Singleton.GetCoins().ToString();
    }

    public override void RecvHit(int damage, bool isResetPosition = false)
    {
		RecvDamage(damage);
		GameDataManager.Singleton.SetHealth(m_Health);

		if (IsAlive())
		{
			if (isResetPosition)
			{
				controller.StopMove();
				controller.MoveToLastCheckpoint();
			}
			else
				controller.HitAnimation();
		}
		else
		{
			controller.DieAnimation();
			Die(isResetPosition);
		}
	}

    public override bool IsPlayer()
    {
		return true;
    }

    public override bool IsAttacking()
    {
		return controller.IsAttacking();
	}

    public void SetCanClimbing(bool canClimb)
    {
		controller.SetCanClimbing(canClimb);
	}
}
