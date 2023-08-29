using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityControllerInterface
{
    void Move(float horizontalAxis);
    bool Jump(float jumpStrength);
    void Attack();
    void HitAnimation();
    void DieAnimation();
}
