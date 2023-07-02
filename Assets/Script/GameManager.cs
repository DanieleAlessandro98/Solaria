using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Singleton;

    private bool m_IsDead;
    private int m_Coins;

    public static GameManager Singleton
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
        m_IsDead = false;
    }

    public bool IsDead()
    {
        return m_IsDead;
    }

    public void Dead()
    {
        m_IsDead = true;
        Application.Quit(); //TODO: Gestire la morte del pg
    }

    private void SaveData()
    {
        GameDataManager.Singleton.SavePlayerData(GameDataManager.NOT_INCLUDED_IN_SAVE, m_Coins);
    }

    private void LoadData()
    {
        m_Coins = GameDataManager.Singleton.GetCurrentCoins();
    }

    public void CollectCoin()
    {
        m_Coins++;
        SaveData();
    }
}
