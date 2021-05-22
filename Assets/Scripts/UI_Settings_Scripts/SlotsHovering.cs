using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DrawingManager;

public class SlotsHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inkPopup;
    public Text textSlot;
    public int id;

    public DrawingManager DrawingManager;

    void Update()
    {
        if (id == DrawingManager.inkSelected)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 217, 255);
            textSlot.color = new Color(0, 217, 255);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            textSlot.color = new Color(255, 255, 255);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inkPopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inkPopup.SetActive(false);
    }
}