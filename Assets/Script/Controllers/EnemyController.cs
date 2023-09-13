using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Inserire classe "Enemy" che gestisce il controller, la vita, il danno (ed altro(?))
internal class EnemyController : MonoBehaviour, EntityControllerInterface
{
    private const float DISTANCE_TOLERANCE = 0.1f;

    //TODO: Mettere nomi statici di queste animazioni da qualche parte (ad esempio anche per "State" e "ChargeAttack2H" in EnemyController)
    private const string ENEMY_ATTACK_ANIMATION = "Attack";

    private Enemy enemy;
    private Vector3 m_SpawnPosition;
    private EEnemyState m_CurrentState;
    private bool m_IsAttacking;
    private Animator m_Animator;
    private float m_Scale;

    [SerializeField]
    private Player m_Player;

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

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        m_Animator = GetComponentInChildren<Animator>();

        m_SpawnPosition = transform.position;
        m_CurrentState = EEnemyState.SpawnPosition;
        m_IsAttacking = false;
        m_Scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetBool("Action"))
            return;

        float distanceSpawnPosition = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        float distancePlayer = Mathf.Abs(transform.position.x - m_Player.GetPositionX());

        switch (m_CurrentState)
        {
            case EEnemyState.SpawnPosition:
                {
                    if (!m_IsFollowPlayer)
                    {
                        if (distancePlayer <= m_AttackRange && !m_IsAttacking)
                        {
                            m_CurrentState = EEnemyState.Attack;

                            if (enemy.IsAlive() && m_Player.IsAlive())
                                StartCoroutine(AttackCoroutine());
                        }
                    }
                    else
                    {
                        if (distancePlayer < m_PlayerFollowRange)
                            m_CurrentState = EEnemyState.Follow;
                    }
                }
                break;

            case EEnemyState.Follow:
                {
                    if (!m_IsFollowPlayer)
                        m_CurrentState = EEnemyState.SpawnPosition;
                    else
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

                                if (enemy.IsAlive() && m_Player.IsAlive())
                                    StartCoroutine(AttackCoroutine());
                            }
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
    }

    public void Move(float horizontalAxis)
    {
        Vector3 targetPosition = new Vector3(horizontalAxis, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, m_MovementSpeed * Time.deltaTime);

        transform.localScale = findDirection(horizontalAxis);
    }

    public bool Jump(float jumpStrength)
    {
        throw new NotImplementedException();
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void HitAnimation()
    {
        m_Animator.SetTrigger("Hit");
    }

    public void DieAnimation()
    {
        m_Animator.SetInteger("State", 9);
    }

    private void MoveToSpawnPosition()
    {
        if (!enemy.IsAlive())
            return;

        m_Animator.SetInteger("State", 2);
        Move(m_SpawnPosition.x);
    }

    private void MoveToPlayerPosition()
    {
        if (!enemy.IsAlive())
            return;

        m_Animator.SetInteger("State", 2);
        Move(m_Player.GetPositionX());
    }

    private Vector3 findDirection(float targetX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(targetX - transform.position.x) * m_Scale;
        return scale;
    }

    private IEnumerator AttackCoroutine()
    {
        m_Animator.SetInteger("State", 0);
        m_Animator.SetTrigger("Attack");
        m_IsAttacking = true;

        yield return new WaitForSeconds(m_AttackDelay);

        m_IsAttacking = false;
    }

    private bool IsSpawnPosition()
    {
        float distance = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        return distance < DISTANCE_TOLERANCE;
    }

    public bool IsAttacking()
    {
        AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        return m_Animator.GetBool("isAttacking") && (stateInfo.IsName(ENEMY_ATTACK_ANIMATION) && stateInfo.normalizedTime < 1f);
    }
}
