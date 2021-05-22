using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SettingsData;

public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text left, right, jump, slot1, slot2, slot3, slot4;

    private GameObject currentKey;

    private Color32 normal = new Color32(104, 186, 214, 255);
    private Color32 selected = new Color32(245, 141, 66, 255);

    public SettingsData SettingsData;
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Left", SettingsData.savedData.keyboard.Left);
        keys.Add("Right", SettingsData.savedData.keyboard.Right);
        keys.Add("Jump", SettingsData.savedData.keyboard.Jump);
        keys.Add("Slot1", SettingsData.savedData.keyboard.Slot1);
        keys.Add("Slot2", SettingsData.savedData.keyboard.Slot2);
        keys.Add("Slot3", SettingsData.savedData.keyboard.Slot3);
        keys.Add("Slot4", SettingsData.savedData.keyboard.Slot4);

        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        slot1.text = keys["Slot1"].ToString();
        slot2.text = keys["Slot2"].ToString();
        slot3.text = keys["Slot3"].ToString();
        slot4.text = keys["Slot4"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        SettingsData.savedData.keyboard.Left = keys["Left"];
        SettingsData.savedData.keyboard.Right = keys["Right"];
        SettingsData.savedData.keyboard.Jump = keys["Jump"];
        SettingsData.savedData.keyboard.Slot1 = keys["Slot1"];
        SettingsData.savedData.keyboard.Slot2 = keys["Slot2"];
        SettingsData.savedData.keyboard.Slot3 = keys["Slot3"];
        SettingsData.savedData.keyboard.Slot4 = keys["Slot4"];
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }
}
