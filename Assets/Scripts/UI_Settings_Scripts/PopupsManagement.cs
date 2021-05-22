using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsManagement : MonoBehaviour
{
    public GameObject infoPopup;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void openInfoPopup()
    {
        infoPopup.SetActive(true);
    }

    public void closeInfoPopup()
    {
        infoPopup.SetActive(false);
    }
}
