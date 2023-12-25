using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для управления звуковыми эффектами и музыкой в игре.
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    /// <summary>
    /// Метод вызывается при пробуждении объекта.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие загрузки сцены
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Запуск мелодии при запуске игры
    /// </summary>
    private void Start()
    {
        if (musicSounds.Length != 0)
        {
            PlayMusic(musicSounds[0].name);
        }
    }

    /// <summary>
    /// Остановка всех текущих звуков при загрузке сцены и запуск мелодии на новой сцене
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instance.sfxSource.Stop();
        Instance.musicSource.Stop();

        if (musicSounds.Length != 0 && scene.name == "Main")
        {
            PlayMusic(musicSounds[0].name);
        }
    }

    /// <summary>
    /// Метод для воспроизведения музыки.
    /// </summary>
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    /// <summary>
    /// Метод для воспроизведения звуковых эффектов.
    /// </summary>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sfx not found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    /// <summary>
    /// Метод для переключения музыки.
    /// </summary>
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        Debug.Log(musicSource.mute);
    }

    /// <summary>
    /// Метод для переключения звуковых эффектов.
    /// </summary>
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        Debug.Log(musicSource.mute);
    }

    /// <summary>
    /// Метод для управления громкостью музыки.
    /// </summary>
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    /// <summary>
    /// Метод для управления громкостью звуковых эффектов.
    /// </summary>
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

