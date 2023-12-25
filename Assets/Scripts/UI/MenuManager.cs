using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ����������� ����� ��� ���������� ���������� ���� � ����.
/// </summary>
public static class MenuManager
{
    public static GameObject mainMenu, settingsMenu, ratingMenu;

    /// <summary>
    /// ����� ��� ������������� ����.
    /// </summary>
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        settingsMenu = canvas.transform.Find("SettingsMenu").gameObject;
        ratingMenu = canvas.transform.Find("RatingMenu").gameObject;
    }

    /// <summary>
    /// ����� ��� �������� ������������� ����.
    /// </summary>
    /// <param name="menu">����, ������� ����� �������.</param>
    /// <param name="callingMenu">����, �� �������� ��� �����.</param>
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

