using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TextManager
{
    public static void LoadPaperInfoText(GameObject textObject, string fileName, Action callback)
    {
        if (!textObject)
            return;

        TextFromFile textFromFile = textObject.GetComponent<TextFromFile>();
        textFromFile.LoadText(fileName, ETextType.PaperMap);

        TextWriterEffect textWriterEffect = textObject.GetComponent<TextWriterEffect>();
        textWriterEffect.writerEffectFinished += callback;
        textWriterEffect.LoadEffect();
    }

    public static void LoadDialogNpcNameText(GameObject textObject, string npcName)
    {
        if (!textObject)
            return;

        textObject.GetComponent<TMP_Text>().text = npcName;
    }

    public static void LoadDialogInfoText(GameObject textObject, string fileName, Action callback)
    {
        if (!textObject)
            return;

        TextFromFile textFromFile = textObject.GetComponent<TextFromFile>();
        textFromFile.LoadText(fileName, ETextType.Dialogs);

        TextWriterEffect textWriterEffect = textObject.GetComponent<TextWriterEffect>();
        textWriterEffect.writerEffectFinished += callback;
        textWriterEffect.LoadEffect();
    }

    public static void LoadSkillInfoText(GameObject textObject, string fileName)
    {
        if (!textObject)
            return;

        TextFromFile textFromFile = textObject.GetComponent<TextFromFile>();
        textFromFile.LoadText(fileName, ETextType.Skills);
    }
}
