using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Singleton;

    private bool m_IsDead;
    private int m_Coins;

    [SerializeField]
    private TextMeshProUGUI m_CoinsText;

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
        SetCoinsText();
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
        SetCoinsText();
        SaveData();
    }

    private void SetCoinsText()
    {
        m_CoinsText.text = m_Coins.ToString();
    }
}
