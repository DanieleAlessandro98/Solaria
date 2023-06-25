using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager m_Singleton;

    public static readonly int FIRST_LEVEL = 0;

    private int m_CurrentLevel;

    public static LevelManager Singleton
    {
        get
        {
            return m_Singleton;
        }
    }

    void Awake()
    {
        if (m_Singleton)
        {
            Destroy(gameObject);
            return;
        }

        m_Singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentLevel = PlayerDataManager.Singleton.LoadPlayerData().GetLevel();
        StartCoroutine(WaitForShowLevel());
    }

    private IEnumerator WaitForShowLevel()
    {
        yield return new WaitForEndOfFrame();

        ShowLevel();
    }

    private void ShowLevel()
    {
        LevelMapManager.Singleton.ShowLevelMap(m_CurrentLevel);
    }

    public int GetCurrentLevel()
    {
        return m_CurrentLevel;
    }
}
