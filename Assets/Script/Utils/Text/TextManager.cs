using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextManager
{
    public static void LoadPaperInfoText(GameObject textObject, string fileName, Action callback)
    {
        if (!textObject)
            return;

        TextFromFile textFromFile = textObject.GetComponent<TextFromFile>();
        textFromFile.LoadText(fileName);

        TextWriterEffect textWriterEffect = textObject.GetComponent<TextWriterEffect>();
        textWriterEffect.writerEffectFinished += callback;
        textWriterEffect.LoadEffect();
    }
}
