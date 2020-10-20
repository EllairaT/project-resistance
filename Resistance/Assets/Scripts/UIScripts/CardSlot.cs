using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : BaseMonobehaviour, IDropHandler
{
    public Card currentCard;
    public CurrentCard currentCardView;
    public bool wasDragged = false;

    public void OnDrop(PointerEventData eventData)
    {
        wasDragged = true;
        PutInSlot(eventData);
    }

    private bool IsSlotEmpty()
    {
        if (transform.childCount > 0)
        {
            return false;
        }
        return true;
    }

    public void PutInSlot(PointerEventData eventData)
    {
        if (IsSlotEmpty() == false)
        {
            transform.GetChild(0).SetParent(eventData.pointerDrag.GetComponent<Draggable>().startingParent);
        }

        Draggable.itemBeingDragged.transform.SetParent(this.transform);

        SetItemDropState(eventData);
        currentCard = eventData.pointerDrag.GetComponent<Draggable>().card;

        if (currentCardView != null)
        {
            currentCardView.UpdateCurrentCardDetails(currentCard);
        }
        wasDragged = false;
    }

    private void SetItemDropState(PointerEventData eventData)
    {
        if (wasDragged)
        {
            eventData.pointerDrag.GetComponent<Draggable>().isDroppedInSlot = true;
        }
        else
        {
            eventData.pointerPress.GetComponent<Draggable>().isDroppedInSlot = true;
        }

        wasDragged = false;
    }
}
