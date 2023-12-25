using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// <summary>
/// Класс для управления геймплеем в игре.
/// </summary>
public class Gameplay : MonoBehaviour
{
    public TextMeshProUGUI ExpressionText;// Текстовое поле для отображения выражения
    public GameObject AnswerContainer;// Контейнер для хранения вариантов ответа
    public TextMeshProUGUI LevelNumberText;// Текстовое поле для отображения номера уровня
    public TextMeshProUGUI ScoreNumberText;// Текстовое поле для отображения количества очков
    public Timer timer;// Таймер для отслеживания оставшегося времени
    public Animator fedor;// Аниматор для управления анимациями Федора

    private int level = 1;// Переменная для хранения текущего уровня
    public static int score = 0;// Переменная для хранения текущего количества очков

    private int pointsPerAnswer = 5;// Количество очков за правильный ответ
    private int timePerAnswer = 5;// Количество времени, добавляемое за правильный ответ
    private int penaltyTime = 7;// Штрафное время за неправильный ответ

    // Создаем словарь для операций
    private Dictionary<string, Func<int, int, int>> operations = new Dictionary<string, Func<int, int, int>>
    {
        { "+", (a, b) => a + b },
        { "-", (a, b) => a - b }
        // Можно добавить новые операции здесь
    };

    /// <summary>
    /// Метод вызывается при старте игры.
    /// </summary>
    void Start()
    {
        GenerateExpressionAndAnswers();
    }

    /// <summary>
    /// Метод для генерации выражения и ответов.
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

        // Выбираем операции из словаря
        List<string> keys = new List<string>(operations.Keys);
        for (int i = 0; i < numberOfTerms - 1; i++)
        {
            operators[i] = keys[UnityEngine.Random.Range(0, keys.Count)];
        }

        string expression = "";
        for (int i = 0; i < numberOfTerms; i++)
        {
            // Если число отрицательное, добавляем скобки вокруг него
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

            // Удаляем старые обработчики событий
            button.onClick.RemoveAllListeners();

            // Добавляем новый обработчик событий
            button.onClick.AddListener(() => CheckAnswer(answerText));
        }
    }


    /// <summary>
    /// Метод для проверки ответа.
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

        // Генерируем новое выражение после каждого ответа
        GenerateExpressionAndAnswers();

        // Сбрасываем состояние кнопки
        Button button = answerText.GetComponentInParent<Button>();
        if (button != null)
        {
            button.OnDeselect(null);
        }
    }

    /// <summary>
    /// Метод для вычисления выражения.
    /// </summary>
    /// <param name="expression">Строка, содержащая выражение для вычисления.</param>
    /// <returns>Результат вычисления выражения.</returns>
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
    /// Метод для преобразования строки в число.
    /// </summary>
    /// <param name="s">Строка, содержащая число.</param>
    /// <returns>Число, полученное из строки.</returns>
    int ParseNumber(string s)
    {
        // Удаляем скобки, если они есть
        s = s.Replace("(", "").Replace(")", "");
        return int.Parse(s);
    }
}

