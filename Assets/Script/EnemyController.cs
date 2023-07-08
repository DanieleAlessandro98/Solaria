using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Inserire classe "Enemy" che gestisce il controller, la vita, il danno (ed altro(?))
public class EnemyController : MonoBehaviour
{
    private const float DISTANCE_TOLERANCE = 0.1f;
    private const int DAMAGE_TO_PLAYER = 1;
    private readonly float DELAY_ENTITY_DIED = 3f;

    private Vector3 m_SpawnPosition;
    private EEnemyState m_CurrentState;
    private bool m_IsAttacking;
    private Health m_Health;

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private PlayerController m_Player;

    [SerializeField]
    private HealthEnemyBoard m_HealthBoard;

    [SerializeField]
    private int m_MaxHealth;

    [SerializeField]
    private bool m_IsFollowPlayer;

    [SerializeField]
    private float m_InitialSpawnPositionRange = 10f;

    [SerializeField]
    private float m_PlayerFollowRange = 5f;

    [SerializeField]
    private float m_AttackRange = 0.9f;

    [SerializeField]
    private float m_AttackDelay = 2f;

    [SerializeField]
    private float m_MovementSpeed = 2f;

    void Awake()
    {
        m_Health = new Health(m_MaxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SpawnPosition = transform.position;
        m_CurrentState = EEnemyState.SpawnPosition;
        m_IsAttacking = false;

        SetHealth(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetBool("Action"))
            return;

        if (!m_IsFollowPlayer)
            return;

        float distanceSpawnPosition = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        float distancePlayer = Mathf.Abs(transform.position.x - m_Player.GetPositionX());

        switch (m_CurrentState)
        {
            case EEnemyState.SpawnPosition:
                {
                    if (distancePlayer < m_PlayerFollowRange)
                        m_CurrentState = EEnemyState.Follow;
                }
                break;

            case EEnemyState.Follow:
                {
                    if (distanceSpawnPosition > m_InitialSpawnPositionRange || distancePlayer > m_PlayerFollowRange)
                        m_CurrentState = EEnemyState.ReturnToSpawPosition;
                    else
                    {
                        if (distancePlayer > m_AttackRange)
                            MoveToPlayerPosition();
                        else if (!m_IsAttacking)
                        {
                            m_CurrentState = EEnemyState.Attack;

                            if (IsAlive() && m_Player.IsAlive())
                                StartCoroutine(AttackCoroutine());
                        }
                    }
                }
                break;

            case EEnemyState.Attack:
                {
                    if (!m_IsAttacking)
                        m_CurrentState = EEnemyState.Follow;
                }
                break;

            case EEnemyState.ReturnToSpawPosition:
                {
                    if (!IsSpawnPosition())
                        MoveToSpawnPosition();
                    else
                    {
                        m_Animator.SetInteger("State", 0);
                        m_CurrentState = EEnemyState.SpawnPosition;
                    }
                }
                break;
        }

        m_HealthBoard.SetRotation(transform.localScale.x);
    }

    public void PlayerHit(int damage)
    {
        if (!IsAlive())
            return;

        SetHealth(damage);
    }

    private void MoveToSpawnPosition()
    {
        if (!IsAlive())
            return;

        m_Animator.SetInteger("State", 2);
        Move(m_SpawnPosition.x);
    }

    private void MoveToPlayerPosition()
    {
        if (!IsAlive())
            return;

        m_Animator.SetInteger("State", 2);
        Move(m_Player.GetPositionX());
    }

    private void Move(float targetX)
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, m_MovementSpeed * Time.deltaTime);

        transform.localScale = findDirection(targetX);
    }

    private Vector3 findDirection(float targetX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(targetX - transform.position.x) * 0.5f;
        return scale;
    }

    private IEnumerator AttackCoroutine()
    {
        m_Animator.SetInteger("State", 0);
        m_Animator.SetTrigger("ChargeAttack2H");
        m_IsAttacking = true;

        yield return new WaitForSeconds(m_AttackDelay);

        m_IsAttacking = false;
    }

    private bool IsSpawnPosition()
    {
        float distance = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        return distance < DISTANCE_TOLERANCE;
    }

    private void SetHealth(int damage)
    {
        m_Health.SetCurrentHealth(damage);
        m_HealthBoard.SetHealth(m_Health.GetCurrentHealth(), m_Health.GetMaxHealth(), m_Health.CalcCurrentHealthPct());

        if (IsAlive())
            m_Animator.SetTrigger("Hit");
        else
            Die();
    }

    public int GetDamage()
    {
        return DAMAGE_TO_PLAYER;
    }

    public bool IsAlive()
    {
        return m_Health.IsAlive();
    }

    public void Die()
    {
        m_Animator.SetInteger("State", 9);
        GetComponent<Collider2D>().enabled = false;
        Invoke("EntityDied", DELAY_ENTITY_DIED);
    }

    private void EntityDied()
    {
        Destroy(gameObject);
    }
}
