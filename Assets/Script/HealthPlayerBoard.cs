using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerBoard : MonoBehaviour
{
    private Material _material;

    void Start()
    {
        _material = GetComponent<Image>().material;
        SetHealth(1f);
    }

    public void SetHealth(float healthPct)
    {
        _material.SetFloat("_FillLevel", healthPct);
    }
}
