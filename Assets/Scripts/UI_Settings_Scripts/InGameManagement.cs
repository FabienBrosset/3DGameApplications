using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManagement : MonoBehaviour
{
    private bool isPaused;
    public GameObject canvasOption;
    public GameObject canvasAudio;
    public GameObject canvasKeyboard;
    public GameObject canvasVideo;
    public GameObject particles;
    public GameObject particles1;
    public GameObject canvasUI;
    public GameObject canvasPause;
    public AudioSource myFX;
    public AudioClip pauseSound;

    public SettingsData SettingsData;
    public KeyBindManager keyBindManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (canvasPause.activeSelf || isPaused == false))
        {
            myFX.PlayOneShot(pauseSound);
            isPaused = !isPaused;

            if (isPaused)
            {
                PauseOn();
            }
            else
            {
                PauseOff();
            }
        }
    }

    void PauseOn()
    {
        canvasUI.SetActive(false);
        canvasPause.SetActive(true);
        particles.SetActive(true);
        particles1.SetActive(true);
    }

    void PauseOff()
    {
        canvasUI.SetActive(true);
        canvasPause.SetActive(false);
        particles.SetActive(false);
        particles1.SetActive(false);
    }

    public void Resume()
    {
        myFX.PlayOneShot(pauseSound);
        PauseOff();
        isPaused = !isPaused;
    }

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

    public void ReturnToPause()
    {
        canvasOption.SetActive(false);
        canvasPause.SetActive(true);
    }

    public void ToOption()
    {
        SettingsData.WriteJsonToDataPersistent();
        canvasPause.SetActive(false);
        canvasKeyboard.SetActive(false);
        canvasAudio.SetActive(false);
        canvasVideo.SetActive(false);
        canvasOption.SetActive(true);
    }
}
