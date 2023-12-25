using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// ����� ��� ���������� �������� ������ � ����.
/// </summary>
public class TextBlink : MonoBehaviour
{
    public float waitTime = 0.8f;
    TextMeshProUGUI text;

    /// <summary>
    /// ����� ���������� ��� ������ ����.
    /// </summary>
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    /// <summary>
    /// �������� ��� ������� ������.
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
    /// ����� ��� ������ ������� ������.
    /// </summary>
    void StartBlinking()
    {
        StartCoroutine("Blink");
    }

    /// <summary>
    /// ����� ��� ��������� ������� ������.
    /// </summary>
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}


