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
    public static readonly int FIRST_COIN = 0;

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

    public void SavePlayerData(int level, int coins)
    {
        if (level != NOT_INCLUDED_IN_SAVE)
            m_PlayerData.SetLevel(level);

        if (coins != NOT_INCLUDED_IN_SAVE)
            m_PlayerData.SetCoins(coins);

        Debug.LogError("coins = " + GetCurrentCoins());
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
            m_PlayerData = new PlayerData(FIRST_LEVEL, FIRST_COIN);
    }

    public int GetCurrentLevel()
    {
        return m_PlayerData.GetLevel();
    }

    public int GetCurrentCoins()
    {
        return m_PlayerData.GetCoins();
    }
}
