using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerBoard : MonoBehaviour, HealthGUI
{
    private Material _material;

    void Awake()
    {
        _material = GetComponent<Image>().material;
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float healthPct = Health.CalcCurrentHealthPct(currentHealth, maxHealth);
        _material.SetFloat("_FillLevel", healthPct);
    }

    public void SetRotation(float scaleX)
    {
        throw new System.NotImplementedException();
    }
}
