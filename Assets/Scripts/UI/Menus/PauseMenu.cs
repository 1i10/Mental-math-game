using UnityEngine;

/// <summary>
/// Класс для управления меню паузы в игре.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Продолжить".
    /// </summary>
    public void onClickBackGame()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        GameManager.OpenMenu(Menu.GAME, gameObject);
    }
}

