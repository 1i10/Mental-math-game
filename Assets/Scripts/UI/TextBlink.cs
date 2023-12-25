using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Класс для управления миганием текста в игре.
/// </summary>
public class TextBlink : MonoBehaviour
{
    public float waitTime = 0.8f;
    TextMeshProUGUI text;

    /// <summary>
    /// Метод вызывается при старте игры.
    /// </summary>
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    /// <summary>
    /// Корутина для мигания текста.
    /// </summary>
    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(waitTime);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(waitTime);
                    break;
            }
        }
    }

    /// <summary>
    /// Метод для начала мигания текста.
    /// </summary>
    void StartBlinking()
    {
        StartCoroutine("Blink");
    }

    /// <summary>
    /// Метод для остановки мигания текста.
    /// </summary>
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}


