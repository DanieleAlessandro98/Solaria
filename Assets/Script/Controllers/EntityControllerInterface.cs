using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityControllerInterface
{
    void Move(float horizontalAxis);
    void Jump();
    void Attack();
    void HitAnimation();
    void DieAnimation();
}
