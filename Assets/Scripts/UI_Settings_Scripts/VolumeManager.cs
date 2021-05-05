using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SettingsData;


public class VolumeManager : MonoBehaviour
{
    public AudioSource FxAudioSource;
    public AudioSource MusicAudioSource;

    public SettingsData settingsData;

    void Update()
    {
        FxAudioSource.volume = settingsData.savedData.audio.FxVolume / 10;
        MusicAudioSource.volume = settingsData.savedData.audio.MusicVolume / 10;
    }

    public void setGeneralVolume(float vol)
    {
        setFxVolume(vol);
        setMusicVolume(vol);
    }

    public void setFxVolume(float vol)
    {
        settingsData.savedData.audio.FxVolume = vol;
    }

    public void setMusicVolume(float vol)
    {
        settingsData.savedData.audio.MusicVolume = vol;
    }
}
