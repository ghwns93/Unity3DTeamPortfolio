using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject Logo;
    public GameObject menu;

    RectTransform logoRectTransform;

    public float scaleFactor = 0.3f;
    public float scaleSpeed = 1.0f;
    public float moveSpeed = 500.0f;

    Vector3 targetPosition = new Vector3(1622f, 908f, 0f);

    float initwidth;
    float initheight;

    float currentwidth;
    float currentheight;

    float currentTime;
    bool timeon = false;

    //////////////////////////////////////////////////

    public GameObject option;

    public GameObject option_Audio;
    public GameObject option_Vidio;

    //////////////////////////////////////////////////

    


    //////////////////////////////////////////////////

    void Start()
    {
        menu.SetActive(false);
        option.SetActive(false);
        option_Audio.SetActive(true);
        option_Vidio.SetActive(false);

        logoRectTransform = Logo.GetComponent<RectTransform>();
        initwidth = logoRectTransform.sizeDelta.x;
        initheight = logoRectTransform.sizeDelta.y;

        currentwidth = initwidth;
        currentheight = initheight;

        currentTime = 0;
        timeon = true;
    }

    private void Update()
    {
        if (timeon)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 3)
            {
                if (currentwidth > initwidth * scaleFactor)
                {
                    float decreaseAmount = currentwidth * scaleSpeed * Time.deltaTime;
                    currentwidth -= decreaseAmount;

                    if (currentwidth <= initwidth * scaleFactor)
                        currentwidth = initwidth * scaleFactor;
                }

                if (currentheight > initheight * scaleFactor)
                {
                    float decreaseAmount = currentheight * scaleSpeed * Time.deltaTime;
                    currentheight -= decreaseAmount;

                    if (currentheight <= initheight * scaleFactor)
                        currentheight = initheight * scaleFactor;
                }

                logoRectTransform.sizeDelta = new Vector2(currentwidth, currentheight);

                logoRectTransform.anchoredPosition3D = Vector3.MoveTowards(logoRectTransform.anchoredPosition3D, targetPosition, moveSpeed * Time.deltaTime);

                if (currentTime >= 4.5f)
                {
                    menu.SetActive(true);
                }

                if (logoRectTransform.transform.position == targetPosition)
                {
                    timeon = false;
                    currentTime = 0;
                }
            }
        }
    }

    //////////////////////////////////////////////////

    public void gamestartClicked()
    {
        LoadingSceneManager.LoadScene("NORSE VILLAGE");
    }

    public void optionClicked()
    {
        menu.SetActive(false);
        option.SetActive(true);
    }

    public void gameexitClicked()
    {
        Application.Quit();
    }

    //////////////////////////////////////////////////

    public void optionVideoClicked()
    {
        option_Vidio.SetActive(true);
        option_Audio.SetActive(false);
    }

    public void optionAudioClicked()
    {
        option_Vidio.SetActive(false);
        option_Audio.SetActive(true);
    }

    public void optionBackClicked()
    {
        option.SetActive(false);
        menu.SetActive(true);
    }

    //////////////////////////////////////////////////

}
