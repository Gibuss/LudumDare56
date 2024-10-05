using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}

