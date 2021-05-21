using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollectInk : MonoBehaviour
{
    public GameObject text;

    void OnDestroy()
    {
        GameObject newText = Instantiate(text, transform.position, Quaternion.identity);
        Destroy(newText, 0.6f);
    }
}
