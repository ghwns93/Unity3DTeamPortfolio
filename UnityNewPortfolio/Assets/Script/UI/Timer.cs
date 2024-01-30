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
    bool questclear = false;        // 임시

    float clearTime = 0f;

    int cleartime_hour = 0;
    int cleartime_minute = 0;
    int cleartime_second = 0;

    private void Update()
    {
        // 퀘스트 진행중일 때 시간 표시
        if (Questing)
        {
            currentTime += Time.deltaTime;
            hour = (int)currentTime / 3600;
            minute = (int)(currentTime / 60) % 60;
            second = (int)currentTime % 60;
            if (text_Time != null) text_Time.text = hour.ToString("00") + " : " + minute.ToString("00") + " : " + second.ToString("00");

            // 퀘스트 클리어 시 결과창에 시간 표시
            if (questclear)
            {
                clearTime = currentTime;

                cleartime_hour = (int)clearTime / 3600;
                cleartime_minute = (int)(clearTime / 60) % 60;
                cleartime_second = (int)clearTime % 60;

                if(Result_Time != null)
                {
                    Result_Time.text = cleartime_hour.ToString("00") + " : " + cleartime_minute.ToString("00") + " : " + cleartime_second.ToString("00");
                }
            }
        }
    }
}
