using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManagement : MonoBehaviour
{
    public GameObject canvasOption;
    public GameObject canvasAudio;
    public GameObject canvasKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ReturnToOption()
    {
        canvasKeyboard.SetActive(false);
        canvasAudio.SetActive(false);
        canvasOption.SetActive(true);
    }
}
