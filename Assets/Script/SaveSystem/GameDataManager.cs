using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private const string SAVE_PATH = "SavedData";

    private const string SAVE_LEVEL_FILE = "level_data";
    private static string LEVEL_PATH = Path.Combine(Path.Combine(Application.streamingAssetsPath, SAVE_PATH), SAVE_LEVEL_FILE);
    private const string SAVE_SESSION_FILE = "session_data";
    private static string SESSION_PATH = Path.Combine(Path.Combine(Application.streamingAssetsPath, SAVE_PATH), SAVE_SESSION_FILE);

    private static GameDataManager m_Singleton;

    private PlayerData m_PlayerLevelData;
    private PlayerData m_PlayerSessionData;

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

        Load(ESaveType.LEVEL);
        Load(ESaveType.SESSION);
    }

    public int GetLevel()
    {
        return m_PlayerSessionData.GetLevel();
    }

    public int GetCoins()
    {
        return m_PlayerSessionData.GetCoins();
    }

    public void IncrementCoins()
    {
        m_PlayerSessionData.SetCoins(m_PlayerSessionData.GetCoins() + 1);
    }

    public void SetLastCheckPointPosition(Vector2 lastCheckPointPosition)
    {
        m_PlayerSessionData.SetLastCheckPointPosition(lastCheckPointPosition);
    }

    public Vector2 GetLastCheckPointPosition()
    {
        return m_PlayerSessionData.GetLastCheckPointPosition();
    }

    public void ResetSessionData()
    {
        m_PlayerSessionData = m_PlayerLevelData;
        Save(ESaveType.SESSION);
    }

    public void SetLevelData()
    {
        m_PlayerLevelData = m_PlayerSessionData;
        Save(ESaveType.LEVEL);
    }

    public void SavePlayerData()
    {
        Save(ESaveType.SESSION);
    }

    public void LoadPlayerData()
    {
        Load(ESaveType.SESSION);
    }

    private void Save(ESaveType saveType)
    {
        string path = FindPath(saveType);
        ref PlayerData playerData = ref FindPlayerData(saveType);

        if (!IsValidPath(path))
            return;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    private void Load(ESaveType saveType)
    {
        string path = FindPath(saveType);
        ref PlayerData playerData = ref FindPlayerData(saveType);

        if (!IsValidPath(path))
            return;

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            playerData = new PlayerData(GameManager.FIRST_LEVEL, GameManager.FIRST_COIN);
            Save(saveType);
        }
    }

    private string FindPath(ESaveType saveType)
    {
        string path;
        if (saveType == ESaveType.LEVEL)
            path = LEVEL_PATH;
        else
            path = SESSION_PATH;

        return path;
    }

    private ref PlayerData FindPlayerData(ESaveType saveType)
    {
        if (saveType == ESaveType.LEVEL)
            return ref m_PlayerLevelData;
        else
            return ref m_PlayerSessionData;
    }

    private bool IsValidPath(string path)
    {
        return path != "";
    }

}
