using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class TextFromFile : MonoBehaviour
{
    private TMP_Text textComponent;
    private string textFilePath;

    private const string TEXT_PATH = "TextResources";
    private const string TEXT_EXTENSION = ".txt";
    private static string RELATIVE_PATH = Path.Combine(Application.streamingAssetsPath, TEXT_PATH);

    public void LoadText(string fileName, ETextType textType)
    {
        textComponent = GetComponent<TMP_Text>();
        textFilePath = Path.Combine(Path.Combine(RELATIVE_PATH, textType.ToString()), fileName + TEXT_EXTENSION);

        if (File.Exists(textFilePath))
            textComponent.text = File.ReadAllText(textFilePath);
        else
            Debug.LogError("LoadText: File not found - " + textFilePath);
    }
}
