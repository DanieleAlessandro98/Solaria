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
    public static readonly float FIRST_VOLUME = 1f;
    private readonly float NEXT_LEVEL_DIALOG = 3f;

    private static GameManager m_Singleton;

    private bool m_IsPlaying;
    private bool m_IsDead;

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

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlaying(false);
        SetDead(false);
    }

    void OnApplicationQuit()
    {
        GameDataManager.Singleton.SavePlayerData();
    }

    public bool IsDead()
    {
        return m_IsDead;
    }

    public bool IsPlaying()
    {
        return m_IsPlaying;
    }

    public void SetDead(bool isDead)
    {
        m_IsDead = isDead;
    }

    public void SetPlaying(bool isPlaying)
    {
        m_IsPlaying = isPlaying;
    }

    //TODO: Rimuovere questi metodi inutili che richiamano solamente GameDataManager. Inserire direttamente il metodo senza passare da GameManager
    public void CollectCoin()
    {
        GameDataManager.Singleton.IncrementCoins();
    }

    public bool IsValidLastCheckPointPosition()
    {
        return GameDataManager.Singleton.GetLastCheckPointPosition() != Vector2.zero;
    }

    public bool IsValidHealth()
    {
        return GameDataManager.Singleton.GetHealth().IsValid();
    }

    public void Dead()
    {
        SceneManager.LoadScene("DiedScene");    //TODO: Gestire la morte del pg
    }

    public void Win()
    {
        GameDataManager.Singleton.SetLevelData();
        StartCoroutine(StartEndLevelDialog());
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelScene");
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
}
