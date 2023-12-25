using UnityEngine;

/// <summary>
/// ����� ��� ���������� ���� �������� � ����.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "�����" � ������ ����.
    /// </summary>
    public void onClickBack()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "�����" � ����.
    /// </summary>
    public void onClickBackGame()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        GameManager.OpenMenu(Menu.GAME, gameObject);
    }
}

