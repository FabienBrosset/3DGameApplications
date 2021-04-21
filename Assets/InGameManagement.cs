using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManagement : MonoBehaviour
{
    private bool isPaused;
    public GameObject canvasUI;
    public GameObject canvasPause;
    public AudioSource myFX;
    public AudioClip pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myFX.PlayOneShot(pauseSound);
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            PauseOn();
        }
        else
        {
            PauseOff();
        }
    }

    void PauseOn()
    {
        canvasUI.SetActive(false);
        canvasPause.SetActive(true);
    }

    void PauseOff()
    {
        canvasPause.SetActive(false);
        canvasUI.SetActive(true);
    }

    public void Resume()
    {
        myFX.PlayOneShot(pauseSound);
        PauseOff();
        isPaused = !isPaused;
    }
}
