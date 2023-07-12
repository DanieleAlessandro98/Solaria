using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerBoard : MonoBehaviour, HealthGUI
{
    private Material _material;

    void Start()
    {
        _material = GetComponent<Image>().material;
        SetHealth(1, 1);
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
