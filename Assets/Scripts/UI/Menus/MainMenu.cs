using UnityEngine;
using UnityEditor;
using TMPro;

/// <summary>
/// Класс для управления главным меню игры.
/// </summary>
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public TextMeshProUGUI scoreField;
    /// <summary>
    /// Объект для управления загрузкой сцен.
    /// </summary>
    public LoadScene loadScene;

    private void Awake()
    {
        string name = PlayerPrefs.GetString("Player");
        playerNameField.text = name == "" ? "Anonymous" : name;

        scoreField.text = "Твой результат - " + PlayerPrefs.GetInt("Score");
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку шестеренки "Настройки".
    /// </summary>
    public void onClickSettings()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        MenuManager.OpenMenu(Menu.SETTINGS, gameObject);
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Топ-5".
    /// </summary>
    public void onClickRating()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.RATING, gameObject);
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Играть".
    /// </summary>
    public void onClickPlay()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        PlayerPrefs.SetString("Player", playerNameField.text);
        string name = playerNameField.text;
        loadScene.Load("Game");
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Выход".
    /// </summary>
    public void onClickExit()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
