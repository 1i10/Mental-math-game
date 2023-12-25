using UnityEngine;
using UnityEditor;
using TMPro;

/// <summary>
/// ����� ��� ���������� ������� ���� ����.
/// </summary>
public class MainMenu : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public TextMeshProUGUI scoreField;
    /// <summary>
    /// ������ ��� ���������� ��������� ����.
    /// </summary>
    public LoadScene loadScene;

    private void Awake()
    {
        string name = PlayerPrefs.GetString("Player");
        playerNameField.text = name == "" ? "Anonymous" : name;

        scoreField.text = "���� ��������� - " + PlayerPrefs.GetInt("Score");
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ ���������� "���������".
    /// </summary>
    public void onClickSettings()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        MenuManager.OpenMenu(Menu.SETTINGS, gameObject);
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "���-5".
    /// </summary>
    public void onClickRating()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        MenuManager.OpenMenu(Menu.RATING, gameObject);
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "������".
    /// </summary>
    public void onClickPlay()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        PlayerPrefs.SetString("Player", playerNameField.text);
        string name = playerNameField.text;
        loadScene.Load("Game");
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "�����".
    /// </summary>
    public void onClickExit()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
