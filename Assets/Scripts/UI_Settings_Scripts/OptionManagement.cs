using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class OptionManagement : MonoBehaviour
{
    public GameObject canvasOption;
    public GameObject canvasAudio;
    public GameObject canvasKeyboard;
    public GameObject canvasVideo;

    public SettingsData SettingsData;
    public KeyBindManager keyBindManager;

    public void ToAudioOption()
    {
        canvasOption.SetActive(false);
        canvasAudio.SetActive(true);
    }

    public void ToKeyboardOption()
    {
        canvasOption.SetActive(false);
        canvasKeyboard.SetActive(true);

    }

    public void ToVideoOption()
    {
        canvasOption.SetActive(false);
        canvasVideo.SetActive(true);
    }

    public void ReturnToOption()
    {
        SettingsData.WriteJsonToDataPersistent();
        canvasKeyboard.SetActive(false);
        canvasAudio.SetActive(false);
        canvasVideo.SetActive(false);
        canvasOption.SetActive(true);
    }
}
