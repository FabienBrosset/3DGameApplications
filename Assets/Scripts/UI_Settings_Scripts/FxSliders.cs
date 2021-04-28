using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSliders : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip sliderFx;

    public void ValueOnChange()
    {
        myFx.PlayOneShot(sliderFx);
    }
}
