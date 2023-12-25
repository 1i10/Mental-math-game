using UnityEngine;

/// <summary>
/// Класс для управления меню настроек в игре.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Назад" в гланом меню.
    /// </summary>
    public void onClickBack()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }

    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Назад" в игре.
    /// </summary>
    public void onClickBackGame()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        GameManager.OpenMenu(Menu.GAME, gameObject);
    }
}

