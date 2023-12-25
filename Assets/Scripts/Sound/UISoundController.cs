using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для управления пользовательским интерфейсом для контроля звука в игре.
/// </summary>
public class UISoundController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    /// <summary>
    /// Установка начальных значений звука
    /// </summary>
    private void Start()
    {
        musicSlider.value = SoundManager.Instance.musicSource.volume;
        sfxSlider.value = SoundManager.Instance.sfxSource.volume;
    }

    /// <summary>
    /// Метод для переключения музыки.
    /// </summary>
    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
    }

    /// <summary>
    /// Метод для переключения звуковых эффектов.
    /// </summary>
    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
    }

    /// <summary>
    /// Метод для управления громкостью музыки.
    /// </summary>
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }

    /// <summary>
    /// Метод для управления громкостью звуковых эффектов.
    /// </summary>
    public void SfxVolume()
    {
        SoundManager.Instance.SFXVolume(sfxSlider.value);
        Debug.Log(sfxSlider);
    }
}