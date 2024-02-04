using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM ����
public enum BGMType { None, Title, InVillage, InField, InBoss }

// SE ����
public enum SEType { OpenShop, BuyClicked, ButtonClick, WrongButtonClick, PotionDrink, OpenChest, EquipChange }

public class SoundManager : MonoBehaviour
{
    public Slider bgmVolumeSlider;      // �ɼ� BGM �������� ��
    public Slider seVolumeSlider;       // �ɼ� SE ���� ���� ��

    private AudioSource bgmAudioSource;
    private AudioSource seAudioSource;

    ///////////////////////////////////////////

    public AudioClip bgmInTitle;        // BGM (Ÿ��Ʋ)
    public AudioClip bgmInVillage;      // BGM (����)
    public AudioClip bgmInField;        // BGM (�ʵ�)
    public AudioClip bgmInBoss;         // BGM (������)

    public AudioClip seOpenShop;            // SE (���� ����)
    public AudioClip seBuyClicked;          // SE (���� ����)
    public AudioClip seButtonClick;         // SE (��ư Ŭ��)
    public AudioClip seWrongButtonClick;    // SE (��Ȱ�� ��ư Ŭ��)
    public AudioClip sePotionDrink;         // SE (����)
    public AudioClip seChest;               // SE (â�� ��/����)
    public AudioClip seEquipChange;         // SE (��� ����/����)

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
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();

        float savedBgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
        float savedSeVolume = PlayerPrefs.GetFloat("SeVolume", 1.0f);

        SetBgmVolume(savedBgmVolume);
        SetSeVolume(savedSeVolume);

        if (bgmVolumeSlider != null )
        {
            bgmVolumeSlider.value = savedBgmVolume;
            bgmVolumeSlider.onValueChanged.AddListener(ChangeBgmVolume);
        }

        if (seVolumeSlider != null)
        {
            seVolumeSlider.value = savedSeVolume;
            seVolumeSlider.onValueChanged.AddListener(ChangeSeVolume);
        }
    }

    void ChangeBgmVolume(float volume)
    {
        SetBgmVolume(volume);
    }

    void ChangeSeVolume(float volume)
    {
        SetSeVolume(volume);
    }

    void SetBgmVolume(float volume)
    {
        bgmAudioSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("BgmVolume", bgmAudioSource.volume);
    }

    void SetSeVolume(float volume)
    {
        seAudioSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SeVolume", seAudioSource.volume);
    }

    ///////////////////////////////////////////

    public void PlayBGM(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;

            if (type == BGMType.Title)
                bgmAudioSource.clip = bgmInTitle;    // Ÿ��Ʋ BGM
            else if (type == BGMType.InVillage)
                bgmAudioSource.clip = bgmInVillage;     // ���� BGM
            else if (type == BGMType.InField)
                bgmAudioSource.clip = bgmInField;     // �ʵ� BGM
            else if (type == BGMType.InBoss)
                bgmAudioSource.clip = bgmInBoss;     // ���� BGM

            bgmAudioSource.Play(); // ���� ���
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
            seAudioSource.PlayOneShot(seOpenShop);
        // ���� ����
        else if (type == SEType.BuyClicked)
            seAudioSource.PlayOneShot(seBuyClicked);
        // ��ư Ŭ��
        else if (type == SEType.ButtonClick)
            seAudioSource.PlayOneShot(seButtonClick);
        // ��Ȱ�� ��ư Ŭ��
        else if (type == SEType.WrongButtonClick)
            seAudioSource.PlayOneShot(seWrongButtonClick);
        // ���� �Դ� �Ҹ�
        else if (type == SEType.PotionDrink)
            seAudioSource.PlayOneShot(sePotionDrink);
        // â�� ������ �Ҹ�
        else if (type == SEType.OpenChest)
            seAudioSource.PlayOneShot(seChest);
        // â�� ������ �Ҹ�
        else if (type == SEType.EquipChange)
            seAudioSource.PlayOneShot(seEquipChange);
    }
}
