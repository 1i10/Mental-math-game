using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Класс для управления загрузкой сцен в игре.
/// </summary>
public class LoadScene : MonoBehaviour
{
    /// <summary>
    /// Имя сцены по умолчанию.
    /// </summary>
    public string sceneName = "Main";

    /// <summary>
    /// Метод вызывается при старте игры.
    /// </summary>
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            Load(sceneName);
        }
    }

    /// <summary>
    /// Метод для загрузки сцены по имени.
    /// </summary>
    /// <param name="name">Имя сцены, которую нужно загрузить.</param>
    public void Load(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
    }

    /// <summary>
    /// Асинхронный метод для загрузки сцены.
    /// </summary>
    /// <param name="sceneName">Имя сцены, которую нужно загрузить.</param>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не будет загружена
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2);
    }
}

