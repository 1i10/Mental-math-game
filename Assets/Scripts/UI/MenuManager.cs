using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Статический класс для управления различными меню в игре.
/// </summary>
public static class MenuManager
{
    public static GameObject mainMenu, settingsMenu, ratingMenu;

    /// <summary>
    /// Метод для инициализации меню.
    /// </summary>
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        settingsMenu = canvas.transform.Find("SettingsMenu").gameObject;
        ratingMenu = canvas.transform.Find("RatingMenu").gameObject;
    }

    /// <summary>
    /// Метод для открытия определенного меню.
    /// </summary>
    /// <param name="menu">Меню, которое нужно открыть.</param>
    /// <param name="callingMenu">Меню, из которого был вызов.</param>
    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (mainMenu == null || settingsMenu == null || ratingMenu == null)
        {
            Init();
        }

        switch (menu)
        {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            case Menu.SETTINGS:
                settingsMenu.SetActive(true);
                break;
            case Menu.RATING:
                ratingMenu.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}

