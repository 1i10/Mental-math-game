using UnityEngine;

/// <summary>
/// Класс для управления меню рейтинга в игре.
/// </summary>
public class RatingMenu : MonoBehaviour
{
    /// <summary>
    /// Метод вызывается при нажатии на кнопку "Назад".
    /// </summary>
    public void onClickBack()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }
}

