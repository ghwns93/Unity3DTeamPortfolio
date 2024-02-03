using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM ����
public enum BGMType { None, Title, InVillage, InField, InBoss }

// SE ����
public enum SEType { OpenShop, BuyClicked, ButtonClick, WrongButtonClick }

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;     // �ɼ� �������� ��

    ///////////////////////////////////////////

    public AudioClip bgmInTitle;        // BGM (Ÿ��Ʋ)
    public AudioClip bgmInVillage;      // BGM (����)
    public AudioClip bgmInField;        // BGM (�ʵ�)
    public AudioClip bgmInBoss;         // BGM (������)

    public AudioClip seOpenShop;            // SE (���� ����)
    public AudioClip seBuyClicked;          // SE (���� ����)
    public AudioClip seButtonClick;         // SE (��ư Ŭ��)
    public AudioClip seWrongButtonClick;    // SE (��Ȱ�� ��ư Ŭ��)

    ///////////////////////////////////////////

    // ù SoundManager�� ������ static ����
    public static SoundManager soundManager;

    //// ���� ��� ���� BGM
    public static BGMType playingBGM = BGMType.None;

    private void Awake()
    {
        // BGM ���
        if (soundManager == null)
        {
            // static ������ �ڱ��ڽ��� ����
            soundManager = this;

            // Scene �� �̵��ص� ������Ʈ�� �ı����� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ������ ���ԵǾ� �ִٸ� ��� �ı�
            Destroy(gameObject);
        }
    }

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

    ///////////////////////////////////////////
    
    public void PlayBGM(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();

            if (type == BGMType.Title)
                audio.clip = bgmInTitle;    // Ÿ��Ʋ BGM
            else if (type == BGMType.InVillage)
                audio.clip = bgmInVillage;     // ���� BGM
            else if (type == BGMType.InField)
                audio.clip = bgmInField;     // �ʵ� BGM
            else if (type == BGMType.InBoss)
                audio.clip = bgmInBoss;     // ���� BGM

            audio.Play(); // ���� ���
        }
    }

    // BGM ����
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    // SE ���
    public void SEPlay(SEType type)
    {
        // ���� ����
        if (type == SEType.OpenShop)
            GetComponent<AudioSource>().PlayOneShot(seOpenShop);
        // ���� ����
        else if (type == SEType.BuyClicked)
            GetComponent<AudioSource>().PlayOneShot(seBuyClicked);
        // ��ư Ŭ��
        else if (type == SEType.ButtonClick)
            GetComponent<AudioSource>().PlayOneShot(seButtonClick);
        // ��Ȱ�� ��ư Ŭ��
        else if (type == SEType.WrongButtonClick)
            GetComponent<AudioSource>().PlayOneShot(seWrongButtonClick);
    }
}
