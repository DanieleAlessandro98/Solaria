using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterface
{
    void SaveLastCheckPointPosition();
    void SetCoinsText();
    void RecvHit(int damage, bool isResetPosition = false);
    int GetDamage();
    void SetCanClimbing(bool canClimb);
}
