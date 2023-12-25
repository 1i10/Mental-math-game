using System.Collections;
using UnityEngine;

/// <summary>
/// Класс для управления меню игры.
/// </summary>
public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject gameOverScreen;
    public LoadScene loadScene;

    /// <summary>
    /// Метод вызывается при пробуждении объекта.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Метод вызывается при нажатии на иконку шестеренки "Настройки".
    /// </summary>
    public void onClickSettings()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        GameManager.OpenMenu(Menu.SETTINGS, gameObject);
    }

    /// <summary>
    /// Метод вызывается при нажатии на иконку "Пауза".
    /// </summary>
    public void onClickPause()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        GameManager.OpenMenu(Menu.PAUSE, gameObject);
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Вернуться в главное меню".
    /// </summary>
    public void onClickBackToMainMenu()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        loadScene.Load("Main");
    }

    /// <summary>
    /// Метод вызывается при окончании игры.
    /// </summary>
    public void GameOver()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("call");
        // Сохраняем итоговый результат
        PlayerPrefs.SetInt("Score", Gameplay.score);

        // Показываем экран завершения игры
        gameOverScreen.SetActive(true);

        // Добавляем запись в рейтинговую таблицу
        HighscoreTable.AddHighscoreEntry(Gameplay.score, PlayerPrefs.GetString("Player"));

        // Загружаем сцену с меню с задержкой
        StartCoroutine(LoadMenuScene());
    }

    /// <summary>
    /// Корутина для загрузки сцены меню.
    /// </summary>
    IEnumerator LoadMenuScene()
    {
        // Ждем 4 секунды
        yield return new WaitForSeconds(4);

        // Загружаем сцену
        loadScene.Load("Main");
    }
}
