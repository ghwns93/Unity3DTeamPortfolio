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

    public Text Result_Time;

    bool Questing = false;
    public bool questclear = false;        // юс╫ц

    float clearTime = 0f;

    int cleartime_hour = 0;
    int cleartime_minute = 0;
    int cleartime_second = 0;

    private void Start()
    {
        currentTime = 0f;
    }

    private void Update()
    {
        if(!questclear) currentTime += Time.deltaTime;
        hour = (int)currentTime / 3600;
        minute = (int)(currentTime / 60) % 60;
        second = (int)currentTime % 60;
        if (text_Time != null) text_Time.text = hour.ToString("00") + " : " + minute.ToString("00") + " : " + second.ToString("00");

        clearTime = currentTime;

        cleartime_hour = (int)clearTime / 3600;
        cleartime_minute = (int)(clearTime / 60) % 60;
        cleartime_second = (int)clearTime % 60;

        if (Result_Time != null)
        {
            Result_Time.text = cleartime_hour.ToString("00") + " : " + cleartime_minute.ToString("00") + " : " + cleartime_second.ToString("00");
        }
    }
}
