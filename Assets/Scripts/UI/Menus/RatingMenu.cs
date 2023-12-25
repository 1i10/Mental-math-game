using UnityEngine;

/// <summary>
/// ����� ��� ���������� ���� �������� � ����.
/// </summary>
public class RatingMenu : MonoBehaviour
{
    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "�����".
    /// </summary>
    public void onClickBack()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }
}

