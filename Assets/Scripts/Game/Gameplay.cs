using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// <summary>
/// ����� ��� ���������� ��������� � ����.
/// </summary>
public class Gameplay : MonoBehaviour
{
    public TextMeshProUGUI ExpressionText;// ��������� ���� ��� ����������� ���������
    public GameObject AnswerContainer;// ��������� ��� �������� ��������� ������
    public TextMeshProUGUI LevelNumberText;// ��������� ���� ��� ����������� ������ ������
    public TextMeshProUGUI ScoreNumberText;// ��������� ���� ��� ����������� ���������� �����
    public Timer timer;// ������ ��� ������������ ����������� �������
    public Animator fedor;// �������� ��� ���������� ���������� ������

    private int level = 1;// ���������� ��� �������� �������� ������
    public static int score = 0;// ���������� ��� �������� �������� ���������� �����

    private int pointsPerAnswer = 5;// ���������� ����� �� ���������� �����
    private int timePerAnswer = 5;// ���������� �������, ����������� �� ���������� �����
    private int penaltyTime = 7;// �������� ����� �� ������������ �����

    // ������� ������� ��� ��������
    private Dictionary<string, Func<int, int, int>> operations = new Dictionary<string, Func<int, int, int>>
    {
        { "+", (a, b) => a + b },
        { "-", (a, b) => a - b }
        // ����� �������� ����� �������� �����
    };

    /// <summary>
    /// ����� ���������� ��� ������ ����.
    /// </summary>
    void Start()
    {
        GenerateExpressionAndAnswers();
    }

    /// <summary>
    /// ����� ��� ��������� ��������� � �������.
    /// </summary>
    void GenerateExpressionAndAnswers()
    {
        int numberOfTerms = level / 7 + 2;
        int[] numbers = new int[numberOfTerms];
        string[] operators = new string[numberOfTerms - 1];

        for (int i = 0; i < numberOfTerms; i++)
        {
            numbers[i] = UnityEngine.Random.Range(-10, 11);
        }

        // �������� �������� �� �������
        List<string> keys = new List<string>(operations.Keys);
        for (int i = 0; i < numberOfTerms - 1; i++)
        {
            operators[i] = keys[UnityEngine.Random.Range(0, keys.Count)];
        }

        string expression = "";
        for (int i = 0; i < numberOfTerms; i++)
        {
            // ���� ����� �������������, ��������� ������ ������ ����
            if (numbers[i] < 0)
            {
                expression += "(" + numbers[i].ToString() + ")";
            }
            else
            {
                expression += numbers[i].ToString();
            }

            if (i < numberOfTerms - 1)
            {
                expression += " " + operators[i] + " ";
            }
        }

        ExpressionText.text = expression;

        int correctAnswer = numbers[0];
        for (int i = 1; i < numberOfTerms; i++)
        {
            correctAnswer = operations[operators[i - 1]](correctAnswer, numbers[i]);
        }

        int[] answers = new int[3];
        answers[0] = correctAnswer;
        answers[1] = correctAnswer + UnityEngine.Random.Range(1, 4);
        answers[2] = correctAnswer - UnityEngine.Random.Range(1, 4);

        answers = answers.OrderBy(x => UnityEngine.Random.value).ToArray();

        for (int i = 0; i < AnswerContainer.transform.childCount; i++)
        {
            Button button = AnswerContainer.transform.GetChild(i).GetComponent<Button>();
            TextMeshProUGUI answerText = button.GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = answers[i].ToString();

            // ������� ������ ����������� �������
            button.onClick.RemoveAllListeners();

            // ��������� ����� ���������� �������
            button.onClick.AddListener(() => CheckAnswer(answerText));
        }
    }


    /// <summary>
    /// ����� ��� �������� ������.
    /// </summary>
    public void CheckAnswer(TextMeshProUGUI answerText)
    {
        int answer = int.Parse(answerText.text);
        int correctAnswer = EvaluateExpression(ExpressionText.text);

        if (answer == correctAnswer)
        {
            fedor.PlayInFixedTime("FedorRight");
            SoundManager.Instance.PlaySFX("right");

            score += pointsPerAnswer;
            ScoreNumberText.text = score.ToString();

            timer.timeRemaining += timePerAnswer;

            level++;
            LevelNumberText.text = level.ToString();

            if (level % 10 == 0)
            {
                pointsPerAnswer += 2;
                timePerAnswer--;
            }
        }
        else
        {
            fedor.PlayInFixedTime("FedorWrong");
            SoundManager.Instance.PlaySFX("wrong");

            timer.timeRemaining -= penaltyTime;
        }

        // ���������� ����� ��������� ����� ������� ������
        GenerateExpressionAndAnswers();

        // ���������� ��������� ������
        Button button = answerText.GetComponentInParent<Button>();
        if (button != null)
        {
            button.OnDeselect(null);
        }
    }

    /// <summary>
    /// ����� ��� ���������� ���������.
    /// </summary>
    /// <param name="expression">������, ���������� ��������� ��� ����������.</param>
    /// <returns>��������� ���������� ���������.</returns>
    int EvaluateExpression(string expression)
    {
        string[] parts = expression.Split(' ');
        int result = ParseNumber(parts[0]);

        for (int i = 1; i < parts.Length; i += 2)
        {
            int number = ParseNumber(parts[i + 1]);
            result = operations[parts[i]](result, number);
        }

        return result;
    }

    /// <summary>
    /// ����� ��� �������������� ������ � �����.
    /// </summary>
    /// <param name="s">������, ���������� �����.</param>
    /// <returns>�����, ���������� �� ������.</returns>
    int ParseNumber(string s)
    {
        // ������� ������, ���� ��� ����
        s = s.Replace("(", "").Replace(")", "");
        return int.Parse(s);
    }
}

