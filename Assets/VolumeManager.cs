using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioSource FxAudioSource;
    public AudioSource MusicAudioSource;

    private float FxVolume = 10;
    private float MusicVolume = 10;

    void Update()
    {
        FxAudioSource.volume = FxVolume / 10;
        MusicAudioSource.volume = MusicVolume / 10;
    }

    public void setGeneralVolume(float vol)
    {
        setFxVolume(vol);
        setMusicVolume(vol);
    }

    public void setFxVolume(float vol)
    {
        FxVolume = vol;
    }

    public void setMusicVolume(float vol)
    {
        MusicVolume = vol;
    }
}
