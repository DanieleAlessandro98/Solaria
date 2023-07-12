using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_Singleton;

    private bool m_isShowingSettings;

    [SerializeField]
    private GameObject m_menuCanvas;

    [SerializeField]
    private GameObject m_settingsCanvas;

    public static MenuManager Singleton
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

    void Start()
    {
        m_isShowingSettings = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeCanvas()
    {
        m_isShowingSettings = !m_isShowingSettings;

        if (!m_isShowingSettings)
        {
            m_menuCanvas.SetActive(true);
            m_settingsCanvas.SetActive(false);
        }
        else
        {
            m_menuCanvas.SetActive(false);
            m_settingsCanvas.SetActive(true);
        }
    }
}
