using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SettingsData;

public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text forward, left, backward, right, jump, crouch;

    private GameObject currentKey;

    private Color32 normal = new Color32(104, 186, 214, 255);
    private Color32 selected = new Color32(245, 141, 66, 255);

    public SettingsData SettingsData;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SettingsData.savedData.keyboard.Forward);
        Debug.Log(SettingsData.savedData.keyboard.Backward);
        Debug.Log(SettingsData.savedData.keyboard.Left);
        Debug.Log(SettingsData.savedData.keyboard.Right);
        Debug.Log(SettingsData.savedData.keyboard.Jump);
        Debug.Log(SettingsData.savedData.keyboard.Crouch);
        keys.Add("Forward", SettingsData.savedData.keyboard.Forward);
        keys.Add("Backward", SettingsData.savedData.keyboard.Backward);
        keys.Add("Left", SettingsData.savedData.keyboard.Left);
        keys.Add("Right", SettingsData.savedData.keyboard.Right);
        keys.Add("Jump", SettingsData.savedData.keyboard.Jump);
        keys.Add("Crouch", SettingsData.savedData.keyboard.Crouch);

        forward.text = keys["Forward"].ToString();
        backward.text = keys["Backward"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        crouch.text = keys["Crouch"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        SettingsData.savedData.keyboard.Forward = keys["Forward"];
        SettingsData.savedData.keyboard.Backward = keys["Backward"];
        SettingsData.savedData.keyboard.Left = keys["Left"];
        SettingsData.savedData.keyboard.Right = keys["Right"];
        SettingsData.savedData.keyboard.Jump = keys["Jump"];
        SettingsData.savedData.keyboard.Crouch = keys["Crouch"];
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Forward))
        {
            Debug.Log("Forward");
        }
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Backward))
        {
            Debug.Log("Backward");
        }
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Left))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Right))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Jump))
        {
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Crouch))
        {
            Debug.Log("Crouch");
        }
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
