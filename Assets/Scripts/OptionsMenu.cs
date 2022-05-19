using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Menu tabs
    public GameObject audiotab;
    public GameObject controlstab;
    public GameObject gameplaytab;
    public GameObject displaytab;
    public GameObject graphicstab;

    public void AudioSelected()
    {
        audiotab.SetActive(true);
        displaytab.SetActive(false);
    }

    public void DisplayoSelected()
    {
        audiotab.SetActive(false);
        displaytab.SetActive(true);
    }


    // Audio
    public AudioMixer masterMixer;

    public void SetMasterVolume (float MasterVolume)
    {
        masterMixer.SetFloat("MasterVolume", MasterVolume);
    }

    public void SetSFXVolume(float SFXVolume)
    {
        masterMixer.SetFloat("SFXVolume", SFXVolume);
    }

    public void SetMusicVolume(float MusicVolume)
    {
        masterMixer.SetFloat("MusicVolume", MusicVolume);
    }
    private void UpdateSliders()
    {
        float value;
        masterMixer.GetFloat("MasterVolume", out value);
        masterSlider.value = value;

        masterMixer.GetFloat("SFXVolume", out value);
        sfxSlider.value = value;

        masterMixer.GetFloat("MusicVolume", out value);
        musicSlider.value = value;
    }

    // Display
    public static FullScreenMode fullScreenMode;
    public void SetScreenMode(int screenmodeIndex)
    {
        if(screenmodeIndex == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if(screenmodeIndex == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (screenmodeIndex == 2)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
    // Volume sliders
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        UpdateSliders();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;

            if (resolutions[i].refreshRate != 60)
            {
                option = resolutions[i].width + " X " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            }

            if (CheckRefreshRate(resolutions[i].refreshRate))
            {
                options.Add(option);
            }

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    // Sets game resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // Check for diffrent refresh rates
    bool CheckRefreshRate(int refreshrate)
    {
        int[] accepted = { 60, 75, 120, 144, 150, 155, 160, 165, 170,
            175, 180, 185, 200, 240, 260, 270, 275, 280, 300, 360, 390 };
        bool found = false;

        foreach (int i in accepted)
        {
            if (i == refreshrate)
            {
                found = true;
            }
        }

        if(found)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

