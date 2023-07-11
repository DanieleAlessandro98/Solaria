using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: Inserire classe "Player" che gestisce il controller, la vita, il danno (ed altro(?))
public class PlayerController : MonoBehaviour
{
	private const float MAX_RUN_SPEED = 8f;
	private const float RUN_SPEED = 5f;
	private const float RUN_SMOOTH_TIME = 5f;
	private const int MAX_HEALTH = 3;
	private readonly float DELAY_ENTITY_DIED = 3f;

	private Vector2 m_Speed;
	private float m_CurrentRunSpeed;
	private float m_CurrentSmoothVelocity;
	private Health m_Health;
	private Vector2 m_LastCheckPointPosition;

	[SerializeField]
	private Rigidbody2D m_Rigidbody2D;

	[SerializeField]
	private Animator m_Animator;

	[SerializeField]
	private GroundChecker m_GroundChecker;

	[SerializeField]
	private float m_JumpStrength = 0.1f;

	[SerializeField]
	private HealthPlayerBoard m_HealthBoard;

	[SerializeField]
	private int m_Damage;

	[SerializeField]
	private TextMeshProUGUI m_CoinsText;

	void Awake()
	{
		m_Health = new Health(MAX_HEALTH);
	}

	// Start is called before the first frame update
	void Start()
	{
		m_Speed = Vector2.zero;
		m_CurrentRunSpeed = 0f;
		m_CurrentSmoothVelocity = 0f;
		m_LastCheckPointPosition = Vector2.zero;

		SetCoinsText();
	}

	// Update is called once per frame
	void Update()
	{
		// Speed
		m_Speed = new Vector2(Mathf.Abs(m_Rigidbody2D.velocity.x), Mathf.Abs(m_Rigidbody2D.velocity.y));

		// Speed Calculations
		m_CurrentRunSpeed = RUN_SPEED;
		if (m_Speed.x >= RUN_SPEED)
			m_CurrentRunSpeed = Mathf.SmoothDamp(m_Speed.x, MAX_RUN_SPEED, ref m_CurrentSmoothVelocity, RUN_SMOOTH_TIME);

		if (Input.GetButtonDown("Jump"))
			Jump();

		if (DialogManager.Singleton.IsDialogOpen())
			StopMove();
		else
			Move(Input.GetAxis("Horizontal"));

		m_Animator.SetFloat("Speed", m_Speed.x);
		m_Animator.SetBool("IsGrounded", m_GroundChecker.IsGrounded());
	}

    public void SaveLastCheckPointPosition()
    {
		m_LastCheckPointPosition = new Vector2(transform.position.x, transform.position.y);
	}

	private void StopMove()
	{
		Vector2 velocity = m_Rigidbody2D.velocity;
		velocity.x = 0f;
		m_Rigidbody2D.velocity = velocity;
	}
	
	private void Move(float horizontalAxis)
	{
		if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
		{
			float speed = m_CurrentRunSpeed;

			Vector2 velocity = m_Rigidbody2D.velocity;
			velocity.x = speed * horizontalAxis;
			m_Rigidbody2D.velocity = velocity;

			if (horizontalAxis > 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Sign(horizontalAxis) * 0.2f;
				transform.localScale = scale;
			}
			else if (horizontalAxis < 0f)
			{
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Sign(horizontalAxis) * 0.2f;
				transform.localScale = scale;
			}

		}
	}

	private void Jump()
	{
		if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
		{
			if (m_GroundChecker.IsGrounded())
			{
				Vector2 velocity = m_Rigidbody2D.velocity;
				velocity.y = m_JumpStrength;
				m_Rigidbody2D.velocity = velocity;

				m_Animator.ResetTrigger("Jump");
				m_Animator.SetTrigger("Jump");
			}
		}
	}

	public void Die(bool isResetPosition)
	{
		GameManager.Singleton.SetDead(true);
		Invoke("EntityDied", isResetPosition ? 0f : DELAY_ENTITY_DIED);
	}

	private void EntityDied()
    {
		GameManager.Singleton.Dead();
	}

	public void EnemyHit(int damage, bool isResetPosition = false)
	{
		m_Health.SetCurrentHealth(damage);
		m_HealthBoard.SetHealth(m_Health.CalcCurrentHealthPct());

		if (IsAlive())
        {
			if (isResetPosition)
            {
				StopMove();
				MoveToLastCheckpoint();
			}
			else
				m_Animator.SetTrigger("Hit");   //TODO: Spostare la direzione del personaggio in base all'hit (sinistra o destra)
		}
		else
        {
			m_Animator.SetTrigger("Die");
			Die(isResetPosition);
		}
	}

    private void MoveToLastCheckpoint()
    {
		transform.position = new Vector2(m_LastCheckPointPosition.x, m_LastCheckPointPosition.y);
	}

	public bool IsMoving()
	{
		return m_Rigidbody2D.velocity.x > 0.1f;
	}

	public bool IsGrounded()
    {
		return m_GroundChecker.IsGrounded();
	}

	public bool IsAlive()
	{
		return m_Health.IsAlive();
	}

	public int GetDamage()
	{
		return m_Damage;
	}

    public float GetPositionX()
    {
		return transform.position.x;
    }

	public void SetCoinsText()
	{
		m_CoinsText.text = GameDataManager.Singleton.GetCoins().ToString();
	}
}
