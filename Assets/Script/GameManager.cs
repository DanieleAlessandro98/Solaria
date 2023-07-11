using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private readonly float NEXT_LEVEL_DIALOG = 3f;

    private static GameManager m_Singleton;

    private bool m_IsDead;
    private int m_Coins;
    private int m_CurrentLevel;

    //[SerializeField]
    //private TextMeshProUGUI m_CoinsText;

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
        GameDataManager.Singleton.SavePlayerData(m_CurrentLevel, m_Coins);
    }

    private void LoadData()
    {
        m_Coins = GameDataManager.Singleton.GetCurrentCoins();
        m_CurrentLevel = GameDataManager.Singleton.GetCurrentLevel();
    }

    public void CollectCoin()
    {
        m_Coins++;
        SetCoinsText();
    }

    private void SetCoinsText()
    {
        //m_CoinsText.text = m_Coins.ToString();
    }

    public void LevelFinished()
    {
        m_CurrentLevel++;
        SaveData();

        StartCoroutine(StartEndLevelDialog());
    }

    private IEnumerator StartEndLevelDialog()
    {
        yield return new WaitForSeconds(NEXT_LEVEL_DIALOG);

        DialogManager.Singleton.StartEndLevelDialog(m_CurrentLevel);
        StartCoroutine(WaitUntilEndLevelDialogIsOpen());
    }

    private IEnumerator WaitUntilEndLevelDialogIsOpen()
    {
        yield return new WaitUntil(() => !DialogManager.Singleton.IsDialogOpen());

        if ((m_CurrentLevel - 1) == 0)
            SceneManager.LoadScene("FutureVisionScene");
        else
            NextLevel();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
