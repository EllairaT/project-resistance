using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : BaseMonobehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (IsSlotEmpty() == false)
        {
            transform.GetChild(0).SetParent(eventData.pointerDrag.GetComponent<Draggable>().startingParent);
        }

        Draggable.itemBeingDragged.transform.SetParent(transform);
        eventData.pointerDrag.GetComponent<Draggable>().isDroppedInSlot = true;
    }

    private bool IsSlotEmpty()
    {
        if (transform.childCount > 0)
        {
            return false;
        }
        return true;
    }
}
