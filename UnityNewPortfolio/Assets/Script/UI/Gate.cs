using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public GameObject Worldmap;

    public float length = 5.0f;

    GameObject player;

    public GameObject canvas;

    bool WorldmapOpened = false;   // false = 닫힌상태  true = 열린상태

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.SetActive(false);
    }

    void Update()
    {
        if (CheckLength(player.transform.position))
        {
            canvas.SetActive(true);

            if (!WorldmapOpened)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for(int i = 0; i < Worldmap.transform.childCount; i++) 
                    {
                        Button btn = Worldmap.transform.GetChild(i).GetComponent<Button>();
                        btn.interactable = false;
                    }

                    if (QuestManager.Instance.nowQuest != null)
                    {
                        for (int i = 0; i < Worldmap.transform.childCount; i++)
                        {
                            Button btn = Worldmap.transform.GetChild(i).GetComponent<Button>();
                            Image image = Worldmap.transform.GetChild(i).GetComponent<Image>();

                            if (image.sprite == QuestManager.Instance.nowQuest.mapImage)
                            {
                                btn.interactable = true;
                            }
                        }
                    }

                    Worldmap.SetActive(true);
                    WorldmapOpened = true;
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Worldmap.SetActive(false);
                    WorldmapOpened = false;
                }
            }
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    // 거리 확인
    bool CheckLength(Vector3 targetPos)
    {
        bool ret = false;
        float d = Vector3.Distance(transform.position, targetPos);

        if (length >= d)
        {
            ret = true;
        }

        return ret;
    }

    public void ChangeScene()
    {
        // BGM 변경
        SoundManager.soundManager.PlayBGM(BGMType.InField);

        //LoadingSceneManager.LoadScene("Isle_01_LP");
        SceneManager.LoadScene("Isle_01_LP");
    }

    public void ChangeSnowScene()
    {
        // BGM 변경
        SoundManager.soundManager.PlayBGM(BGMType.InBoss);

        SceneManager.LoadScene("SNOWLANDS");
    }
}
