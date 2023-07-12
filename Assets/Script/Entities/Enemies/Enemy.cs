using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, EnemyInterface
{
    private readonly float DELAY_ENTITY_DIED = 3f;
    private const int DAMAGE_TO_PLAYER = 1;

    private EnemyController controller;

    [SerializeField]
    private int m_MaxHealth;

    [SerializeField]
    private bool m_IsFinalBoss;

    private void Awake()
    {
        SetHealth(new Health(m_MaxHealth));
        SetHealthGUI(GetComponentInChildren<HealthEnemyBoard>());
    }

    void Start()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        SetHealthRotation();
    }

    public override void Die(bool isResetPosition = false)
    {
        controller.DieAnimation();

        GetComponent<Collider2D>().enabled = false;

        if (m_IsFinalBoss)
            GameManager.Singleton.Win();

        Invoke("EntityDied", DELAY_ENTITY_DIED);
    }

    public override void EntityDied()
    {
        Destroy(gameObject);
    }

    public override int GetDamage()
    {
        return DAMAGE_TO_PLAYER;
    }

    public override bool IsAttacking()
    {
        return controller.IsAttacking();
    }

    public override bool IsPlayer()
    {
        return false;
    }

    public override void RecvHit(int damage, bool isResetPosition = false)
    {
        RecvDamage(damage);

        if (IsAlive())
            controller.HitAnimation();
        else
            Die();
    }
}
