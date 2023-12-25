using System.Collections;
using UnityEngine;

/// <summary>
/// ����� ��� ���������� ���� ����.
/// </summary>
public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject gameOverScreen;
    public LoadScene loadScene;

    /// <summary>
    /// ����� ���������� ��� ����������� �������.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ ���������� "���������".
    /// </summary>
    public void onClickSettings()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        GameManager.OpenMenu(Menu.SETTINGS, gameObject);
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "�����".
    /// </summary>
    public void onClickPause()
    {
        SoundManager.Instance.PlaySFX("clickSettings");

        GameManager.OpenMenu(Menu.PAUSE, gameObject);
    }

    /// <summary>
    /// ����� ���������� ��� ������� �� ������ "��������� � ������� ����".
    /// </summary>
    public void onClickBackToMainMenu()
    {
        SoundManager.Instance.PlaySFX("clickMenu");

        loadScene.Load("Main");
    }

    /// <summary>
    /// ����� ���������� ��� ��������� ����.
    /// </summary>
    public void GameOver()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("call");
        // ��������� �������� ���������
        PlayerPrefs.SetInt("Score", Gameplay.score);

        // ���������� ����� ���������� ����
        gameOverScreen.SetActive(true);

        // ��������� ������ � ����������� �������
        HighscoreTable.AddHighscoreEntry(Gameplay.score, PlayerPrefs.GetString("Player"));

        // ��������� ����� � ���� � ���������
        StartCoroutine(LoadMenuScene());
    }

    /// <summary>
    /// �������� ��� �������� ����� ����.
    /// </summary>
    IEnumerator LoadMenuScene()
    {
        // ���� 4 �������
        yield return new WaitForSeconds(4);

        // ��������� �����
        loadScene.Load("Main");
    }
}
