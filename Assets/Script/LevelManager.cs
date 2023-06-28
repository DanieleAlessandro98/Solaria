using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager m_Singleton;

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
        LoadData();
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

    public void StartLevel()
    {
        SceneManager.LoadScene("GameLevel" + m_CurrentLevel);
    }

    public int GetCurrentLevel()
    {
        return m_CurrentLevel;
    }

    //TODO: SaveData e LoadData in interfaccia
    private void SaveData()
    {
        GameDataManager.Singleton.SavePlayerData(m_CurrentLevel, GameDataManager.NOT_INCLUDED_IN_SAVE);
    }

    private void LoadData()
    {
        m_CurrentLevel = GameDataManager.Singleton.GetCurrentLevel();
    }
}
