using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Health m_Health;
    private HealthGUI m_HealthGUI;

    protected void SetHealth(Health health)
    {
        m_Health = health;
    }

    protected void SetHealthGUI(HealthGUI healthGUI)
    {
        m_HealthGUI = healthGUI;
    }

    public void RecvDamage(int damage)
    {
        m_Health.SetCurrentHealth(damage);
        m_HealthGUI.SetHealth(m_Health.GetCurrentHealth(), m_Health.GetMaxHealth());
    }

    public void SetHealthRotation()
    {
        m_HealthGUI.SetRotation(transform.localScale.x);
    }

    public bool IsAlive()
    {
        return m_Health.IsAlive();
    }

    public abstract void Die(bool isResetPosition);
    public abstract void EntityDied();
    public abstract int GetDamage();
    public abstract void RecvHit(int damage, bool isResetPosition = false);
    public abstract bool IsPlayer();
    public abstract bool IsAttacking();
}
