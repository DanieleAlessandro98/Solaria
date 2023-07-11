using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly int FIRST_LEVEL = 0;
    public static readonly int FIRST_COIN = 0;

    private readonly float NEXT_LEVEL_DIALOG = 3f;

    private static GameManager m_Singleton;

    private bool m_IsDead;

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
        SetDead(false);
        SetCoinsText();
    }

    void OnApplicationQuit()
    {
        GameDataManager.Singleton.SavePlayerData();
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
        GameDataManager.Singleton.ResetSessionData();
        SceneManager.LoadScene("DiedScene");    //TODO: Gestire la morte del pg
    }

    public void CollectCoin()
    {
        GameDataManager.Singleton.IncrementCoins();
        SetCoinsText();
    }

    private void SetCoinsText()
    {
        m_CoinsText.text = GameDataManager.Singleton.GetCoins().ToString();
    }

    public void LevelFinished()
    {
        GameDataManager.Singleton.SetLevelData();
        StartCoroutine(StartEndLevelDialog());
    }

    private IEnumerator StartEndLevelDialog()
    {
        yield return new WaitForSeconds(NEXT_LEVEL_DIALOG);

        DialogManager.Singleton.StartEndLevelDialog(GameDataManager.Singleton.GetLevel());
        StartCoroutine(WaitUntilEndLevelDialogIsOpen());
    }

    private IEnumerator WaitUntilEndLevelDialogIsOpen()
    {
        yield return new WaitUntil(() => !DialogManager.Singleton.IsDialogOpen());

        if ((GameDataManager.Singleton.GetLevel() - 1) == 0)
            SceneManager.LoadScene("FutureVisionScene");
        else
            NextLevel();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
