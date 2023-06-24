using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextWriterEffect : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    private string m_FullText;
    private string m_CurrentText;

    [SerializeField]
    private float delay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }

    public void LoadEffect()
    {
        m_FullText = m_TextComponent.text;
        m_CurrentText = "";
        StartCoroutine(WriterEffect());
    }

    IEnumerator WriterEffect()
    {
        for (int i = 0; i <= m_FullText.Length; i++)
        {
            m_CurrentText = m_FullText.Substring(0, i);
            m_TextComponent.text = m_CurrentText;

            yield return new WaitForSeconds(delay);
        }
    }
}
