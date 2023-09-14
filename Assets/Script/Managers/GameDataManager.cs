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
    private const string SAVE_SETTINGS_FILE = "settings_data";
    private static string SETTINGS_PATH = Path.Combine(Path.Combine(Application.streamingAssetsPath, SAVE_PATH), SAVE_SETTINGS_FILE);

    private static GameDataManager m_Singleton;

    private SettingsData m_SettingsData;
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

        LoadSettings(ESaveType.SETTINGS);
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

    public Vector2 GetLastCheckPointPosition()
    {
        return m_PlayerSessionData.GetLastCheckPointPosition();
    }

    public Health GetHealth()
    {
        return m_PlayerSessionData.GetHealth();
    }

    public bool IsSkillUnlocked(ESkillName skillName)
    {
        return m_PlayerSessionData.IsSkillUnlocked(skillName);
    }
    
    public float GetVolume()
    {
        return m_SettingsData.GetVolume();
    }

    public void IncrementCoins()
    {
        m_PlayerSessionData.SetCoins(m_PlayerSessionData.GetCoins() + 1);
    }

    public void SetLastCheckPointPosition(Vector2 lastCheckPointPosition)
    {
        m_PlayerSessionData.SetLastCheckPointPosition(lastCheckPointPosition);
    }

    public void SetHealth(Health health)
    {
        m_PlayerSessionData.SetHealth(health);
    }

    public void UnlockSkill(ESkillName skillName)
    {
        m_PlayerSessionData.UnlockSkill(skillName);
    }

    public void SetVolume(float volume)
    {
        m_SettingsData.SetVolume(volume);
        SaveSettings(ESaveType.SETTINGS);
    }

    public void ResetSessionData()
    {
        m_PlayerLevelData.SetLastCheckPointPosition(Vector2.zero);
        m_PlayerLevelData.SetHealth(m_PlayerLevelData.InitHealth());

        m_PlayerSessionData.SetLevel(m_PlayerLevelData.GetLevel());
        m_PlayerSessionData.SetCoins(m_PlayerLevelData.GetCoins());
        m_PlayerSessionData.SetLastCheckPointPosition(m_PlayerLevelData.GetLastCheckPointPosition());
        m_PlayerSessionData.SetHealth(m_PlayerLevelData.GetHealth());
        m_PlayerSessionData.SetSkills(m_PlayerLevelData.GetSkills());
        Save(ESaveType.SESSION);
    }

    public void SetLevelData()
    {
        m_PlayerSessionData.SetLevel(m_PlayerSessionData.GetLevel() + 1);
        m_PlayerSessionData.SetLastCheckPointPosition(Vector2.zero);
        m_PlayerSessionData.SetHealth(m_PlayerSessionData.InitHealth());

        m_PlayerLevelData.SetCoins(m_PlayerSessionData.GetCoins());
        m_PlayerLevelData.SetLevel(m_PlayerSessionData.GetLevel());
        m_PlayerLevelData.SetLastCheckPointPosition(m_PlayerSessionData.GetLastCheckPointPosition());
        m_PlayerLevelData.SetHealth(m_PlayerSessionData.GetHealth());
        m_PlayerLevelData.SetSkills(m_PlayerSessionData.GetSkills());
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

    private void SaveSettings(ESaveType saveType)
    {
        string path = FindPath(saveType);
        if (!IsValidPath(path))
            return;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(fileStream, m_SettingsData);
        fileStream.Close();
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

    private void LoadSettings(ESaveType saveType)
    {
        string path = FindPath(saveType);
        if (!IsValidPath(path))
            return;

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            m_SettingsData = (SettingsData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            m_SettingsData = new SettingsData();
            SaveSettings(saveType);
        }
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
            playerData = new PlayerData();
            Save(saveType);
        }
    }

    private string FindPath(ESaveType saveType)
    {
        string path;
        if (saveType == ESaveType.LEVEL)
            path = LEVEL_PATH;
        else if (saveType == ESaveType.SESSION)
            path = SESSION_PATH;
        else
            path = SETTINGS_PATH;

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
