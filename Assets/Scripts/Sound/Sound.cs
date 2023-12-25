using UnityEngine;

/// <summary>
/// Класс для хранения информации о звуковых клипах.
/// </summary>
[System.Serializable]
public class Sound
{
    /// <summary>
    /// Имя звукового клипа.
    /// </summary>
    public string name;

    /// <summary>
    /// Звуковой клип.
    /// </summary>
    public AudioClip clip;
}

