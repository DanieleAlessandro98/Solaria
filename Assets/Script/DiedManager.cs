using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedManager : MonoBehaviour
{
    private static DiedManager m_Singleton;

    private int m_CurrentLevel;

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

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameLevel" + m_CurrentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadData()
    {
        m_CurrentLevel = GameDataManager.Singleton.GetCurrentLevel();
    }
}
