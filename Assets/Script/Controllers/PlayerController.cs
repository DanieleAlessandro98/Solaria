using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//TODO: Inserire classe "Player" che gestisce il controller, la vita, il danno (ed altro(?))
internal class PlayerController : MonoBehaviour, EntityControllerInterface
{
	private const float MAX_RUN_SPEED = 8f;
	private const float RUN_SPEED = 5f;
	private const float RUN_SMOOTH_TIME = 5f;

	private Vector2 m_Speed;
	private float m_CurrentRunSpeed;
	private float m_CurrentSmoothVelocity;
	private Rigidbody2D m_Rigidbody2D;
	private Animator m_Animator;

	[SerializeField]
    private GroundChecker m_GroundChecker;

	[SerializeField]
	private float m_JumpStrength = 0.1f;

    // Start is called before the first frame update
    void Start()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Animator = GetComponent<Animator>();

		m_Speed = Vector2.zero;
		m_CurrentRunSpeed = 0f;
		m_CurrentSmoothVelocity = 0f;
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

		if (DialogManager.Singleton.IsDialogOpen())
			StopMove();

		if (Input.GetButtonDown("Jump"))
			Jump(m_JumpStrength);

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			Attack();

		m_Animator.SetFloat("Speed", m_Speed.x);
		m_Animator.SetBool("IsGrounded", m_GroundChecker.IsGrounded());
	}

	void FixedUpdate()
    {
		if (!DialogManager.Singleton.IsDialogOpen())
			Move(Input.GetAxis("Horizontal"));
	}

	public void Move(float horizontalAxis)
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

	public bool Jump(float jumpStrength)
	{
		if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
		{
			if (m_GroundChecker.IsGrounded())
			{
				Vector2 velocity = m_Rigidbody2D.velocity;
				velocity.y = jumpStrength;
				m_Rigidbody2D.velocity = velocity;

				m_Animator.ResetTrigger("Jump");
				m_Animator.SetTrigger("Jump");

				return true;
			}
		}

		return false;
	}

	public void Attack()
	{
		if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
		{
			if (!IsAttacking() && !IsMoving() && IsGrounded())
				m_Animator.SetTrigger("Attack");
		}
	}

	public void HitAnimation()
	{
		m_Animator.SetTrigger("Hit");   //TODO: Spostare la direzione del personaggio in base all'hit (sinistra o destra)
	}

	public void DieAnimation()
	{
		m_Animator.SetTrigger("Die");
	}

	public void StopMove()
	{
		Vector2 velocity = m_Rigidbody2D.velocity;
		velocity.x = 0f;
		m_Rigidbody2D.velocity = velocity;
	}

	public void MoveToLastCheckpoint()
	{
		transform.position = GameDataManager.Singleton.GetLastCheckPointPosition();
	}

	private bool IsMoving()
	{
		return m_Rigidbody2D.velocity.x > 0.1f;
	}

	private bool IsGrounded()
	{
		return m_GroundChecker.IsGrounded();
	}

	public bool IsAttacking()
	{
		return m_Animator.GetBool("isAttacking");
	}
}
