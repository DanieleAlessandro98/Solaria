using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemyBoard : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthRect;

    [SerializeField]
    private Text healthText;

    public void SetHealth(int currentHealth, int maxHealth, float healthPct)
    {
        healthRect.localScale = new Vector3(healthPct, healthRect.localScale.y, healthRect.localScale.z);
        healthText.text = Health.GetHealthFormatting(currentHealth, maxHealth);
    }

    public void SetRotation(float scaleX)
    {
        float angle = scaleX > 0 ? 0f : 180f;
        healthText.rectTransform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
