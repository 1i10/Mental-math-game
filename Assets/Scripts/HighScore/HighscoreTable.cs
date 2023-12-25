using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

/// <summary>
/// ����� ��� ���������� �������� �������� � ����.
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
    /// ����� ���������� ��� ����������� �������.
    /// </summary>
    private void Awake()
    {
        rowTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // ��� ����������� �������, �������������
            Debug.Log("highscore table with default values...");
            for (int i = 0; i < countVisibleRows; i++)
            {
                AddHighscoreEntry(0, "-");
            }
            // ������������
            jsonString = PlayerPrefs.GetString("scoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // ���������� ������ ������� �� �����
        highscores.highscoreEntryList = highscores.highscoreEntryList.OrderByDescending(row => row.score).ToList();

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, rowContainer, highScoreEntryTransformList);
        }
    }

    /// <summary>
    /// ����� ��� �������� ������������� ������ � ������� ��������.
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

        // ��������� ������� �����
        if (rank == 1)
        {
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }

        transformList.Add(entryTransform);
    }

    /// <summary>
    /// ����� ��� ���������� ������ � ������� ��������. ���������� ����� �� ����������� (���������� ���������� �������)
    /// </summary>
    public static void AddHighscoreEntry(int score, string name)
    {
        // �������� ����������� ��������
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // ��� ����������� �������, �������������
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // ����� ������������ ������
        HighscoreEntry existingEntry = highscores.highscoreEntryList.Find(entry => entry.name == name);

        if (existingEntry != null)
        {
            // ���� ����� ���� ����, ��������� ������������ ������
            if (score > existingEntry.score)
            {
                existingEntry.score = score;
            }
        }
        else
        {
            // �������� ����� ������ HighscoreEntry
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

            // ���������� ����� ������ � Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);
        }

        // ���������� ����������� Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ����� ��� �������� ������ ������� � ������� ��������.
    /// </summary>
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /// <summary>
    /// ����� ��� �������� ������ � ������� ��������.
    /// </summary>
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

