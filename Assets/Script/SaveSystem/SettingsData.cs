using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    private float volume;

    public SettingsData()
    {
        this.volume = GameManager.FIRST_VOLUME;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }
}
