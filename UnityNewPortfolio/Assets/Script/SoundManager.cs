using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM 종류
public enum BGMType { None, Title, InVillage, InField, InBoss }

// SE 종류
public enum SEType { OpenShop, BuyClicked, ButtonClick, WrongButtonClick }

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;     // 옵션 볼륨조절 바

    ///////////////////////////////////////////

    public AudioClip bgmInTitle;        // BGM (타이틀)
    public AudioClip bgmInVillage;      // BGM (마을)
    public AudioClip bgmInField;        // BGM (필드)
    public AudioClip bgmInBoss;         // BGM (보스전)

    public AudioClip seOpenShop;            // SE (상점 오픈)
    public AudioClip seBuyClicked;          // SE (상점 구매)
    public AudioClip seButtonClick;         // SE (버튼 클릭)
    public AudioClip seWrongButtonClick;    // SE (비활성 버튼 클릭)

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
                audio.clip = bgmInTitle;    // 타이틀 BGM
            else if (type == BGMType.InVillage)
                audio.clip = bgmInVillage;     // 마을 BGM
            else if (type == BGMType.InField)
                audio.clip = bgmInField;     // 필드 BGM
            else if (type == BGMType.InBoss)
                audio.clip = bgmInBoss;     // 보스 BGM

            audio.Play(); // 사운드 재생
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
            GetComponent<AudioSource>().PlayOneShot(seOpenShop);
        // 상점 구매
        else if (type == SEType.BuyClicked)
            GetComponent<AudioSource>().PlayOneShot(seBuyClicked);
        // 버튼 클릭
        else if (type == SEType.ButtonClick)
            GetComponent<AudioSource>().PlayOneShot(seButtonClick);
        // 비활성 버튼 클릭
        else if (type == SEType.WrongButtonClick)
            GetComponent<AudioSource>().PlayOneShot(seWrongButtonClick);
    }
}
