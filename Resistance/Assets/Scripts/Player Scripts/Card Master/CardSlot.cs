using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : BaseMonobehaviour, IDropHandler
{
    public GameObject snap;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            Debug.Log("snap?");
            eventData.pointerDrag.GetComponent<RectTransform>().position = snap.GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<RectTransform>().transform.parent = snap.transform.parent;
        }
    }
}
