using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private static DialogManager m_Singleton;

    private bool isDialogOpen;

    [SerializeField]
    private GameObject m_DialogCanvas;

    [SerializeField]
    private GameObject m_DialogNpcNameText;

    [SerializeField]
    private GameObject m_DialogInfoText;

    [SerializeField]
    private GameObject m_DialogCloseButton;

    public static DialogManager Singleton
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
        isDialogOpen = false;
        m_DialogCanvas.SetActive(false);
        m_DialogCloseButton.SetActive(false);
    }

    public void StartDialog(EDialogName dialogName)
    {
        m_DialogCanvas.SetActive(true);

        TextManager.LoadDialogNpcNameText(m_DialogNpcNameText, findDialogNpcName(dialogName));
        TextManager.LoadDialogInfoText(m_DialogInfoText, findDialogFileName(dialogName), WriterEffectCompletedCallback);

        isDialogOpen = true;
    }

    private string findDialogNpcName(EDialogName dialogName)
    {
        return DialogDictionary.dialogNpcName[dialogName];
    }

    private string findDialogFileName(EDialogName dialogName)
    {
        return DialogDictionary.dialogFile[dialogName];
    }

    private void WriterEffectCompletedCallback()
    {
        m_DialogCloseButton.SetActive(true);
    }

    public bool IsDialogOpen()
    {
        return isDialogOpen;
    }

    public void CloseDialog()
    {
        isDialogOpen = false;
        m_DialogCanvas.SetActive(false);
        m_DialogCloseButton.SetActive(false);
    }
}
