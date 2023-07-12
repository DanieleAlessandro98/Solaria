using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface
{
    void RecvHit(int damage, bool isResetPosition = false);
    int GetDamage();
}
