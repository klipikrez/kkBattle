using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class graphics_options : MonoBehaviour
{
    int width, height, hz = 60;
    bool fullscreen = true, VSync;


    public GameObject fpsCounter;
    TMP_InputField input;

    public Slider fpsSlider;

    public Toggle fullScreenToggle;
    public Toggle VSyncToggle;

    void Start()
    {
        input = fpsCounter.GetComponent<TMP_InputField>();
        width = Screen.width;
        height = Screen.height;
        if (PlayerPrefs.HasKey("fullScreen"))
        {

            switch (PlayerPrefs.GetInt("fullScreen"))
            {
                case 0:
                    fullscreen = false;                   
                    break;
                case 1:
                    fullscreen = true;
                    break;
                default:
                    //trombon bbbBBBBBBBBB
                    break;
            }
        }
        if (PlayerPrefs.HasKey("screenWidth"))
        {
            width = PlayerPrefs.GetInt("screenWidth");
        }
        if (PlayerPrefs.HasKey("screenHeight"))
        {
            height = PlayerPrefs.GetInt("screenHeight");
        }
        if (PlayerPrefs.HasKey("maxFps"))
        {
            hz = PlayerPrefs.GetInt("maxFps");
        }
        if (PlayerPrefs.HasKey("VSync"))
        {
            switch (PlayerPrefs.GetInt("VSync"))
            {
                case 0:
                    SetVSync(false);
                    VSync = false;
                    break;
                case 1:
                    SetVSync(true);
                    VSync = true;
                    break;
                default:
                    //trombon bbbBBBBBBBBB
                    break;
            }
        }
        UpdateResolution();
        UpdateUi();
    }

    public void FullScreen(bool value)
    {
        Screen.fullScreen = value;
        fullscreen = value;
        UpdateResolution();
        if(value)
        {
            PlayerPrefs.SetInt("fullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 0);
        }
    }

    public void SetResolutionX(string x)
    {
        width = int.Parse(x);
        UpdateResolution();
        PlayerPrefs.SetInt("screenWidth", int.Parse(x));
    }

    public void SetResolutionY(string y)
    {
        height = int.Parse(y);
        UpdateResolution();
        PlayerPrefs.SetInt("screenHeight", int.Parse(y));
    }

    public void SetMaxFramerateString(string value)
    {
        int val2 = int.Parse(value);

        if(val2 < (int)fpsSlider.minValue)
        {
            val2 = (int)fpsSlider.minValue;
        }
        else
        {
            if (val2 > (int)fpsSlider.maxValue)
            {
                val2 = (int)fpsSlider.maxValue;
            }
        }

        SetMaxFramerate(val2);
    }

    public void SetMaxFramerate(float value)
    {
        hz = (int)value;
        input.text = ((int)value).ToString();
        fpsSlider.value = value;
        UpdateResolution();
        PlayerPrefs.SetInt("maxFps", (int)value);
    }


    public void SetVSync(bool value)
    {
        VSync = value;
        if (value)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("VSync", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("VSync", 0);
        }
        UpdateResolution();
    }

    void UpdateResolution()
    {
        Screen.SetResolution(width, height, fullscreen, hz);
        Application.targetFrameRate = hz;
    }


    void UpdateUi()
    {
        fpsSlider.value = hz;
        input.text = ((int)hz).ToString();
        fullScreenToggle.isOn = fullscreen;
        VSyncToggle.isOn = VSync;
    }


}
