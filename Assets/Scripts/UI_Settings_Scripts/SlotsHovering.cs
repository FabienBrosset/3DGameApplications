/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsHovering : MonoBehaviour
{
    public GameObject inkPopup;

    void OnMouseOver()
    {
        inkPopup.SetActive(true);
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        inkPopup.SetActive(false);
        Debug.Log("Mouse is over GameObject.");
    }
}*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotsHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inkPopup;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inkPopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inkPopup.SetActive(false);
    }
}