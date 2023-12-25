using UnityEngine;
using TMPro;

/// <summary>
/// Класс для управления таймером в игре.
/// </summary>
public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private bool lastSecondsRunning = false;

    /// <summary>
    /// Начало отсчета таймера при старте игры.
    /// </summary>
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        SoundManager.Instance.PlaySFX("call");
    }

    /// <summary>
    /// Метод для обновления времени.
    /// </summary>
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                DisplayTime(timeRemaining);

                timerIsRunning = false;

                GameMenu.instance.GameOver();
            }

            if (timeRemaining <= 10f)
            {
                if (!lastSecondsRunning)
                {
                    timeText.color = Color.red;

                    SoundManager.Instance.PlaySFX("timer10");

                    lastSecondsRunning = true;
                }
            }
            else if (timeRemaining > 10f && lastSecondsRunning) 
            {
                timeText.color = Color.white;

                SoundManager.Instance.sfxSource.Stop();

                lastSecondsRunning = false;
            }
        }
    }

    /// <summary>
    /// Метод для отображения оставшегося времени.
    /// </summary>
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

