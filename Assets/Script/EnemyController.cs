using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float DISTANCE_TOLERANCE = 0.1f;

    private Vector3 m_SpawnPosition;
    private EEnemyState m_CurrentState;
    private bool m_IsAttacking;

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private Transform m_Player;

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
        m_SpawnPosition = transform.position;
        m_CurrentState = EEnemyState.SpawnPosition;
        m_IsAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetBool("Action"))
            return;

        float distanceSpawnPosition = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        float distancePlayer = Mathf.Abs(transform.position.x - m_Player.position.x);

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
    }

    private void MoveToSpawnPosition()
    {
        m_Animator.SetInteger("State", 2);
        Move(m_SpawnPosition.x);
    }

    private void MoveToPlayerPosition()
    {
        m_Animator.SetInteger("State", 2);
        Move(m_Player.position.x);
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

    public void PlayerHit()
    {
        m_Animator.SetTrigger("Hit");
    }

    private bool IsSpawnPosition()
    {
        float distance = Mathf.Abs(transform.position.x - m_SpawnPosition.x);
        return distance < DISTANCE_TOLERANCE;
    }
}
