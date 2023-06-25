using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private static PlayerDataManager m_Singleton;

    private const string SAVE_PATH = "SavedData";
    private const string SAVE_FILE = "data";

    private static string PATH = Path.Combine(Path.Combine(Application.streamingAssetsPath, SAVE_PATH), SAVE_FILE);

    public static PlayerDataManager Singleton
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

    public void SavePlayerData(bool firstSave = false)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(PATH, FileMode.Create);

        PlayerData playerData = new PlayerData(firstSave ? LevelManager.FIRST_LEVEL : LevelManager.Singleton.GetCurrentLevel());

        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(PATH))
            SavePlayerData(true);
        
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(PATH, FileMode.Open);

        PlayerData result = (PlayerData)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();

        return result;
    }
}
