using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

//TODO: Inserire classe "Player" che gestisce il controller, la vita, il danno (ed altro(?))
internal class PlayerController : MonoBehaviour, EntityControllerInterface
{
	private const float RUN_SPEED = 5f;
	private const float SPRINT_SPEED = 8f;

	private Vector2 m_Speed;
	private Rigidbody2D m_Rigidbody2D;
	private Animator m_Animator;
	private bool m_CanClimb;

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

		m_CanClimb = false;
	}

	// Update is called once per frame
	void Update()
	{
		// Speed
		m_Speed = new Vector2(Mathf.Abs(m_Rigidbody2D.velocity.x), Mathf.Abs(m_Rigidbody2D.velocity.y));

		if (DialogManager.Singleton.IsDialogOpen())
			StopMove();

		if (Input.GetButtonDown("Jump"))
			Jump(m_JumpStrength);

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			Attack();

		if (Input.GetKeyDown(KeyCode.UpArrow) && m_CanClimb)
			Climb();
		else
        {
			if (Input.GetKeyUp(KeyCode.UpArrow) || (!m_CanClimb && IsClimbing()))
				StopClimb();
		}

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
			float speed = m_Animator.GetBool("isSprinting") ? SPRINT_SPEED : RUN_SPEED;

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
            {
				// TODO: Migliorare la selezione dell'attacco (non basarsi sul livello, ma ad esempio se possiede una spada o meno)
				if (GameDataManager.Singleton.GetLevel() == 0)
					m_Animator.SetTrigger("Attack");
				else
					m_Animator.SetTrigger("AttackWithSword");
			}
		}
	}

	public void Climb()
	{
		if (!GameManager.Singleton.IsDead() && !DialogManager.Singleton.IsDialogOpen())
        {
			if (m_CanClimb)
			{
				m_Animator.ResetTrigger("Jump");
				m_Animator.SetTrigger("Climb");

				Vector2 velocity = m_Rigidbody2D.velocity;
				velocity.y = RUN_SPEED;
				m_Rigidbody2D.velocity = velocity;

				m_Rigidbody2D.gravityScale = 0f;
			}
		}
	}

	public void StopClimb()
	{
		m_Rigidbody2D.gravityScale = 1f;
		m_Animator.SetBool("isClimbing", false);
	}

	public void HitAnimation()
	{
		m_Animator.SetTrigger("Hit");   //TODO: Spostare la direzione del personaggio in base all'hit (sinistra o destra)
	}

	public void DieAnimation()
	{
		m_Animator.SetTrigger("Die");
	}

	public void SprintAnimation()
	{
		StopMove();
		m_Animator.SetTrigger("Sprint");
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

	public void SetCanClimbing(bool canClimb)
	{
		m_CanClimb = canClimb;
	}

	private bool IsMoving()
	{
		return m_Rigidbody2D.velocity.x > 0.1f;
	}

	public bool IsGrounded()
	{
		return m_GroundChecker.IsGrounded();
	}

	public bool IsAttacking()
	{
		return m_Animator.GetBool("isAttacking");
	}

	public bool IsSprinting()
	{
		return m_Animator.GetBool("isSprinting");
	}

	public bool IsClimbing()
	{
		return m_Animator.GetBool("isClimbing");
	}
}
