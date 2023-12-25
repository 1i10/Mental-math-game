using UnityEngine;

/// <summary>
/// ����� ��� ���������� ���� ����� � ����.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "����������".
    /// </summary>
    public void onClickBackGame()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        GameManager.OpenMenu(Menu.GAME, gameObject);
    }
}

