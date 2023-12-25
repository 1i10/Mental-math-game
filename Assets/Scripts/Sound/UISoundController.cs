using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����� ��� ���������� ���������������� ����������� ��� �������� ����� � ����.
/// </summary>
public class UISoundController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    /// <summary>
    /// ��������� ��������� �������� �����
    /// </summary>
    private void Start()
    {
        musicSlider.value = SoundManager.Instance.musicSource.volume;
        sfxSlider.value = SoundManager.Instance.sfxSource.volume;
    }

    /// <summary>
    /// ����� ��� ������������ ������.
    /// </summary>
    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
    }

    /// <summary>
    /// ����� ��� ������������ �������� ��������.
    /// </summary>
    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
    }

    /// <summary>
    /// ����� ��� ���������� ���������� ������.
    /// </summary>
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }

    /// <summary>
    /// ����� ��� ���������� ���������� �������� ��������.
    /// </summary>
    public void SfxVolume()
    {
        SoundManager.Instance.SFXVolume(sfxSlider.value);
        Debug.Log(sfxSlider);
    }
}