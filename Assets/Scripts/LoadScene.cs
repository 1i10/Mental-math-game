using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// ����� ��� ���������� ��������� ���� � ����.
/// </summary>
public class LoadScene : MonoBehaviour
{
    /// <summary>
    /// ��� ����� �� ���������.
    /// </summary>
    public string sceneName = "Main";

    /// <summary>
    /// ����� ���������� ��� ������ ����.
    /// </summary>
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            Load(sceneName);
        }
    }

    /// <summary>
    /// ����� ��� �������� ����� �� �����.
    /// </summary>
    /// <param name="name">��� �����, ������� ����� ���������.</param>
    public void Load(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
    }

    /// <summary>
    /// ����������� ����� ��� �������� �����.
    /// </summary>
    /// <param name="sceneName">��� �����, ������� ����� ���������.</param>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // ����, ���� ����� �� ����� ���������
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2);
    }
}

