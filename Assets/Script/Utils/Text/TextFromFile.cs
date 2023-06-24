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

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    public void LoadText(string fileName)
    {
        textFilePath = Path.Combine(RELATIVE_PATH, fileName + TEXT_EXTENSION);

        if (File.Exists(textFilePath))
            textComponent.text = File.ReadAllText(textFilePath);
        else
            Debug.LogError("LoadText: File not found - " + textFilePath);
    }
}
