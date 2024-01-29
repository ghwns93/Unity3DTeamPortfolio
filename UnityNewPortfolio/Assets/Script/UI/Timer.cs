using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;

    int hour = 0;
    int minute = 0;
    int second = 0;

    public Text text_Time;

    private void Update()
    {
        currentTime += Time.deltaTime;

        hour = (int)currentTime / 3600;
        minute = (int)(currentTime / 60) % 60;
        second = (int)currentTime % 60;
        if(text_Time != null) text_Time.text = hour.ToString("00") + " : " + minute.ToString("00") + " : " + second.ToString("00");
    }
}
