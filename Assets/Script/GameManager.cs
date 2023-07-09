using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SetDead(false);
    }

    public void SetDead(bool isDead)
    {
        m_IsDead = isDead;
    }

    public bool IsDead()
    {
        return m_IsDead;
    }

    public void Dead()
    {
        SceneManager.LoadScene("DiedScene");    //TODO: Gestire la morte del pg
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
