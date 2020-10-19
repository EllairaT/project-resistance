using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : BaseMonobehaviour, IDropHandler
{
    //TODO get item's original parent slot

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if(!Item)
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);
            Item.GetComponent<Draggable>().isDroppedInSlot = true;
        }
    }
}
