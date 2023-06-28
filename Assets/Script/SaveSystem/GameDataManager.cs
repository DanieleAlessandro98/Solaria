using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private static GameDataManager m_Singleton;

    private const string SAVE_PATH = "SavedData";
    private const string SAVE_FILE = "data";
    private static string PATH = Path.Combine(Path.Combine(Application.streamingAssetsPath, SAVE_PATH), SAVE_FILE);

    public static readonly int NOT_INCLUDED_IN_SAVE = -1;
    public static readonly int FIRST_LEVEL = 0;

    private PlayerData m_PlayerData;

    public static GameDataManager Singleton
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

        DontDestroyOnLoad(gameObject);
        LoadPlayerData();
    }

    public void SavePlayerData(int level)
    {
        if (level != NOT_INCLUDED_IN_SAVE)
            m_PlayerData.SetLevel(level);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(PATH, FileMode.Create);

        binaryFormatter.Serialize(fileStream, m_PlayerData);
        fileStream.Close();
    }

    public void LoadPlayerData()
    {
        if (File.Exists(PATH))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(PATH, FileMode.Open);

            m_PlayerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
            m_PlayerData = new PlayerData(FIRST_LEVEL);
    }

    public int GetCurrentLevel()
    {
        return m_PlayerData.GetLevel();
    }
}
