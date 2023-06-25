using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    private static LevelMapManager m_Singleton;

    [SerializeField]
    private GameObject[] m_CharacterSpriteLevel;

    [SerializeField]
    private GameObject m_PaperImage;
    [SerializeField]
    private GameObject[] m_PaperLevelNameImage;
    [SerializeField]
    private GameObject m_PaperInfoText;
    [SerializeField]
    private GameObject m_PaperPlayButton;

    public static LevelMapManager Singleton
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
        HideAll();
    }

    private void HideAll()
    {
        foreach (GameObject characterSpriteLevel in m_CharacterSpriteLevel)
            characterSpriteLevel.SetActive(false);

        m_PaperImage.SetActive(false);

        foreach (GameObject paperLevelNameImage in m_PaperLevelNameImage)
            paperLevelNameImage.SetActive(false);

        m_PaperInfoText.SetActive(false);
        m_PaperPlayButton.SetActive(false);
    }

    public void ShowLevelMap(int level)
    {
        m_CharacterSpriteLevel[level].SetActive(true);
        m_PaperImage.SetActive(true);
        m_PaperLevelNameImage[level].SetActive(true);
        m_PaperInfoText.SetActive(true);

        TextManager.LoadPaperInfoText(m_PaperInfoText, findLevelFileName(level), WriterEffectCompletedCallback);
    }

    private string findLevelFileName(int level)
    {
        ELevelFileNameInfo enumLevel = (ELevelFileNameInfo)Enum.ToObject(typeof(ELevelFileNameInfo), level);
        string levelFileName = enumLevel.ToString();

        return levelFileName;
    }

    private void WriterEffectCompletedCallback()
    {
        m_PaperPlayButton.SetActive(true);
    }
}
