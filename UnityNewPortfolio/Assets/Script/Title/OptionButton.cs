using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    bool isOn = false;

    public Text text_onoff;
    RectTransform image_onoff;

    public void Buttonclicked()
    {
        isOn = !isOn;

        if(isOn)
        {
            text_onoff.text = "ON";
            image_onoff.GetComponent<RectTransform>().position = new Vector3(32, 0, 0);
        }
        else
        {
            text_onoff.text = "OFF";
            image_onoff.GetComponent<RectTransform>().position = new Vector3(-32, 0, 0);
        }
    }
}
