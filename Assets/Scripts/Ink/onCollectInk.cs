using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollectInk : MonoBehaviour
{
    public GameObject text;

    public void displayText()
    {
        GameObject newText = Instantiate(text, transform.position, Quaternion.identity);
        Destroy(newText, 0.6f);
    }
}
