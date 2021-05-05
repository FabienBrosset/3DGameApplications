using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SettingsData;

public class ScreenSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    Resolution[] resolutions;
    
    public SettingsData settingsData;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = settingsData.savedData.video.ResolutionIndex;
        qualityDropdown.value = settingsData.savedData.video.QualityIndex;
        fullscreenToggle.isOn = settingsData.savedData.video.isFullScreen;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        settingsData.savedData.video.ResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[settingsData.savedData.video.ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        settingsData.savedData.video.QualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(settingsData.savedData.video.QualityIndex, true);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        settingsData.savedData.video.isFullScreen = isFullScreen;
        Screen.fullScreen = settingsData.savedData.video.isFullScreen;
    }
}
