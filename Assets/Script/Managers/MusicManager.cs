using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager m_Singleton;

    private AudioSource m_AudioSource;

    [SerializeField]
    private AudioClip[] m_SceneMusic;

    [SerializeField]
    private string[] m_ScenesWithoutMusic;

    public static MusicManager Singleton
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
        m_AudioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource.volume = GameDataManager.Singleton.GetVolume();

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_AudioSource.isPlaying && !IsSceneInScenesWithoutMusic())
            m_AudioSource.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.isLoaded || System.Array.Exists(m_ScenesWithoutMusic, s => s == scene.name))
        {
            if (m_AudioSource.isPlaying)
                m_AudioSource.Stop();

            return;
        }

        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        foreach (AudioClip music in m_SceneMusic)
        {
            if (music.name == sceneName)
            {
                m_AudioSource.Stop();
                m_AudioSource.clip = music;
                m_AudioSource.Play();
                break;
            }
        }
    }

    private bool IsSceneInScenesWithoutMusic()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return System.Array.Exists(m_ScenesWithoutMusic, s => s == currentScene.name);
    }

    public float GetAudioVolume()
    {
        return m_AudioSource.volume;
    }

    public void SetAudioVolume(float newVolume)
    {
        m_AudioSource.volume = newVolume;
        GameDataManager.Singleton.SetVolume(newVolume);
    }
}
