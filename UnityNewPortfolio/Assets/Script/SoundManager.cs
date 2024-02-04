using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM 종류
public enum BGMType { None, Title, InVillage, InField, InBoss }

// SE 종류
public enum SEType { OpenShop, BuyClicked, ButtonClick, WrongButtonClick, PotionDrink, OpenChest, EquipChange }

public class SoundManager : MonoBehaviour
{
    public Slider bgmVolumeSlider;      // 옵션 BGM 볼륨조절 바
    public Slider seVolumeSlider;       // 옵션 SE 볼륨 조절 바

    private AudioSource bgmAudioSource;
    private AudioSource seAudioSource;

    ///////////////////////////////////////////

    public AudioClip bgmInTitle;        // BGM (타이틀)
    public AudioClip bgmInVillage;      // BGM (마을)
    public AudioClip bgmInField;        // BGM (필드)
    public AudioClip bgmInBoss;         // BGM (보스전)

    public AudioClip seOpenShop;            // SE (상점 오픈)
    public AudioClip seBuyClicked;          // SE (상점 구매)
    public AudioClip seButtonClick;         // SE (버튼 클릭)
    public AudioClip seWrongButtonClick;    // SE (비활성 버튼 클릭)
    public AudioClip sePotionDrink;         // SE (포션)
    public AudioClip seChest;               // SE (창고 온/오프)
    public AudioClip seEquipChange;         // SE (장비 장착/해제)

    ///////////////////////////////////////////

    // 첫 SoundManager를 저장할 static 변수
    public static SoundManager soundManager;

    //// 현재 재생 중인 BGM
    public static BGMType playingBGM = BGMType.None;

    private void Awake()
    {
        // BGM 재생
        if (soundManager == null)
        {
            // static 변수에 자기자신을 저장
            soundManager = this;

            // Scene 이 이동해도 오브젝트를 파기하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 정보가 삽입되어 있다면 즉시 파기
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
                bgmAudioSource.clip = bgmInTitle;    // 타이틀 BGM
            else if (type == BGMType.InVillage)
                bgmAudioSource.clip = bgmInVillage;     // 마을 BGM
            else if (type == BGMType.InField)
                bgmAudioSource.clip = bgmInField;     // 필드 BGM
            else if (type == BGMType.InBoss)
                bgmAudioSource.clip = bgmInBoss;     // 보스 BGM

            bgmAudioSource.Play(); // 사운드 재생
        }
    }

    // BGM 정지
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    // SE 재생
    public void SEPlay(SEType type)
    {
        // 상점 오픈
        if (type == SEType.OpenShop)
            seAudioSource.PlayOneShot(seOpenShop);
        // 상점 구매
        else if (type == SEType.BuyClicked)
            seAudioSource.PlayOneShot(seBuyClicked);
        // 버튼 클릭
        else if (type == SEType.ButtonClick)
            seAudioSource.PlayOneShot(seButtonClick);
        // 비활성 버튼 클릭
        else if (type == SEType.WrongButtonClick)
            seAudioSource.PlayOneShot(seWrongButtonClick);
        // 포션 먹는 소리
        else if (type == SEType.PotionDrink)
            seAudioSource.PlayOneShot(sePotionDrink);
        // 창고 여닫이 소리
        else if (type == SEType.OpenChest)
            seAudioSource.PlayOneShot(seChest);
        // 창고 여닫이 소리
        else if (type == SEType.EquipChange)
            seAudioSource.PlayOneShot(seEquipChange);
    }
}
