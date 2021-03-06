using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class SettingsData : MonoBehaviour
{
    [Serializable]
    public class VideoData
    {
        public int ResolutionIndex;
        public int QualityIndex;
        public bool isFullScreen;
    }

    [Serializable]
    public class AudioData
    {
        public float FxVolume;
        public float MusicVolume;
    }

    [Serializable]
    public class KeyboardData
    {
        public KeyCode Left;
        public KeyCode Right;
        public KeyCode Jump;
        public KeyCode Slot1;
        public KeyCode Slot2;
        public KeyCode Slot3;
        public KeyCode Slot4;
    }

    [Serializable]
    public class SaveData
    {
        public AudioData audio;
        public KeyboardData keyboard;
        public VideoData video;
    }

    public SaveData savedData;

    void Start()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "SettingsData.json");

        if (!System.IO.File.Exists(dataPath))
        {
            savedData.audio.FxVolume = 10;
            savedData.audio.MusicVolume = 10;
            savedData.keyboard.Left = KeyCode.Q;
            savedData.keyboard.Right = KeyCode.D;
            savedData.keyboard.Jump = KeyCode.Space;
            savedData.keyboard.Slot1 = KeyCode.Alpha1;
            savedData.keyboard.Slot2 = KeyCode.Alpha2;
            savedData.keyboard.Slot3 = KeyCode.Alpha3;
            savedData.keyboard.Slot4 = KeyCode.Alpha4;
            savedData.video.isFullScreen = true;
            savedData.video.QualityIndex = 3;
            savedData.video.ResolutionIndex = Screen.resolutions.Length - 1;
            WriteJsonToDataPersistent();
            setScreenData();
        }
        else
            savedData = JsonUtility.FromJson<SaveData>(File.ReadAllText(dataPath));
    }

    public void WriteJsonToDataPersistent()
    {
        string path = Path.Combine(Application.persistentDataPath, "SettingsData.json");
        string content = JsonUtility.ToJson(savedData);

        FileStream stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
    }

    public void setScreenData()
    {
        Resolution[] resolutions;
        resolutions = Screen.resolutions;

        Resolution resolution = resolutions[savedData.video.ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        QualitySettings.SetQualityLevel(savedData.video.QualityIndex, true);
        Screen.fullScreen = savedData.video.isFullScreen;
    }
}