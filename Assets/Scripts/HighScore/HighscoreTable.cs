using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

/// <summary>
/// Класс для управления таблицей рекордов в игре.
/// </summary>
public class HighscoreTable : MonoBehaviour
{
    [SerializeField]
    private Transform rowContainer;
    [SerializeField]
    private Transform rowTemplate;
    [SerializeField]
    private int countVisibleRows = 0;

    private List<Transform> highScoreEntryTransformList;

    /// <summary>
    /// Метод вызывается при пробуждении объекта.
    /// </summary>
    private void Awake()
    {
        rowTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // Нет сохраненной таблицы, инициализация
            Debug.Log("highscore table with default values...");
            for (int i = 0; i < countVisibleRows; i++)
            {
                AddHighscoreEntry(0, "-");
            }
            // Перезагрузка
            jsonString = PlayerPrefs.GetString("scoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Сортировка списка записей по очкам
        highscores.highscoreEntryList = highscores.highscoreEntryList.OrderByDescending(row => row.score).ToList();

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, rowContainer, highScoreEntryTransformList);
        }
    }

    /// <summary>
    /// Метод для создания трансформации записи в таблице рекордов.
    /// </summary>
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        if (transformList.Count >= countVisibleRows)
        {
            return;
        }

        Transform entryTransform = Instantiate(rowTemplate, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rank.ToString();

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        // Выделение первого места
        if (rank == 1)
        {
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }

        transformList.Add(entryTransform);
    }

    /// <summary>
    /// Метод для добавления записи в таблицу рекордов. Одинаковые имена не добавляются (происходит обновление рекорда)
    /// </summary>
    public static void AddHighscoreEntry(int score, string name)
    {
        // Загрузка сохраненных рекордов
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // Нет сохраненной таблицы, инициализация
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Поиск существующей записи
        HighscoreEntry existingEntry = highscores.highscoreEntryList.Find(entry => entry.name == name);

        if (existingEntry != null)
        {
            // Если новый счет выше, обновляем существующую запись
            if (score > existingEntry.score)
            {
                existingEntry.score = score;
            }
        }
        else
        {
            // Создание новой записи HighscoreEntry
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

            // Добавление новой записи в Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);
        }

        // Сохранение обновленных Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Класс для хранения списка записей в таблице рекордов.
    /// </summary>
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /// <summary>
    /// Класс для хранения записи в таблице рекордов.
    /// </summary>
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

