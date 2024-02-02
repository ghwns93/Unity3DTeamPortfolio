using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        SetVolume(savedVolume);

        if(volumeSlider != null )
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }        
    }

    public void ChangeVolume(float volume)
    {
        SetVolume(volume);
    }

    void SetVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
    }
}
