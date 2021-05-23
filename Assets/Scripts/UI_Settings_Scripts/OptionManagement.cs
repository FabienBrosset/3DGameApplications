using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class OptionManagement : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject canvasSelector;
    public GameObject canvasOption;
    public GameObject canvasAudio;
    public GameObject canvasKeyboard;
    public GameObject canvasVideo;
    public GameObject canvasHowToPlay;
    public GameObject particles;
    public GameObject particles1;

    public SettingsData SettingsData;
    public KeyBindManager keyBindManager;

    public void ToAudioOption()
    {
        canvasOption.SetActive(false);
        canvasAudio.SetActive(true);
    }

    public void ToSelector()
    {
        canvasMenu.SetActive(false);
        canvasSelector.SetActive(true);
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

    public void ReturnToMainMenu()
    {
        canvasSelector.SetActive(false);
        canvasOption.SetActive(false);
        canvasHowToPlay.SetActive(false);
        canvasMenu.SetActive(true);
        particles.SetActive(false);
        particles1.SetActive(false);
    }

    public void ToHowToPlay()
    {
        canvasMenu.SetActive(false);
        canvasHowToPlay.SetActive(true);
        particles.SetActive(true);
        particles1.SetActive(true);

    }

    public void ToOption()
    {
        SettingsData.WriteJsonToDataPersistent();
        canvasMenu.SetActive(false);
        canvasKeyboard.SetActive(false);
        canvasAudio.SetActive(false);
        canvasVideo.SetActive(false);
        canvasOption.SetActive(true);
        particles.SetActive(true);
        particles1.SetActive(true);
    }
}
