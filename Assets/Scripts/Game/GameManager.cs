using UnityEngine;

/// <summary>
/// ����������� ����� ��� ���������� ���������� ���� � ����.
/// </summary>
public static class GameManager
{
    public static GameObject gameField, settingsMenu, pauseMenu;

    /// <summary>
    /// ����� ��� ������������� ����.
    /// </summary>
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameField = canvas.transform.Find("GameField").gameObject;
        settingsMenu = canvas.transform.Find("SettingsMenu").gameObject;
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
    }

    /// <summary>
    /// ����� ��� �������� ������������� ����.
    /// </summary>
    /// <param name="menu">����, ������� ����� �������.</param>
    /// <param name="callingMenu">����, �� �������� ��� �����.</param>
    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (gameField == null || settingsMenu == null || pauseMenu == null)
        {
            Init();
        }

        switch (menu)
        {
            case Menu.GAME:
                gameField.SetActive(true);
                SoundManager.Instance.sfxSource.Play();
                Time.timeScale = 1f;
                break;
            case Menu.SETTINGS:
                settingsMenu.SetActive(true);
                SoundManager.Instance.sfxSource.Pause();
                Time.timeScale = 0f;
                break;
            case Menu.PAUSE:
                pauseMenu.SetActive(true);
                SoundManager.Instance.sfxSource.Pause();
                Time.timeScale = 0f;
                break;
        }

        callingMenu.SetActive(false);
    }
}

