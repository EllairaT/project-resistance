using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class CardMenuSlot : BaseMonobehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().position = snap.GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<RectTransform>().transform.parent = this.transform;
        }
    }
}
