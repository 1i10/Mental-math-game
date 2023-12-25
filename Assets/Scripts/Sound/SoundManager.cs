using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// ����� ��� ���������� ��������� ��������� � ������� � ����.
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    /// <summary>
    /// ����� ���������� ��� ����������� �������.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // ������������� �� ������� �������� �����
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// ������ ������� ��� ������� ����
    /// </summary>
    private void Start()
    {
        if (musicSounds.Length != 0)
        {
            PlayMusic(musicSounds[0].name);
        }
    }

    /// <summary>
    /// ��������� ���� ������� ������ ��� �������� ����� � ������ ������� �� ����� �����
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
    /// ����� ��� ��������������� ������.
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
    /// ����� ��� ��������������� �������� ��������.
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
    /// ����� ��� ������������ ������.
    /// </summary>
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        Debug.Log(musicSource.mute);
    }

    /// <summary>
    /// ����� ��� ������������ �������� ��������.
    /// </summary>
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        Debug.Log(musicSource.mute);
    }

    /// <summary>
    /// ����� ��� ���������� ���������� ������.
    /// </summary>
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    /// <summary>
    /// ����� ��� ���������� ���������� �������� ��������.
    /// </summary>
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

