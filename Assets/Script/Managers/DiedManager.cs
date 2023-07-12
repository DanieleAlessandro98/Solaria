using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedManager : MonoBehaviour
{
    private static DiedManager m_Singleton;

    public static DiedManager Singleton
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

    private void Start()
    {
        GameDataManager.Singleton.ResetSessionData();
    }

    public void TryAgain()
    {
        GameManager.Singleton.SetDead(false);
        SceneManager.LoadScene("GameLevel" + GameDataManager.Singleton.GetLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
