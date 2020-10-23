using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardSlot : BaseMonobehaviour, IDropHandler
{
    public Card currentCard;

    public delegate void ChangeEvent(bool slotState, Card c);
    public static event ChangeEvent changeEvent;
    public bool isSlotEmpty = true;

    public bool isPlayableCardSlot;
    public GameObject playableCardSlot;

    #region DropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).SetParent(eventData.pointerDrag.GetComponent<Draggable>().startingParent);
            }

            Draggable.itemBeingDragged.transform.SetParent(transform);
            SetCard(eventData.pointerDrag.GetComponent<Draggable>().card);
        }
    }
    #endregion

    public bool IsSlotEmpty()
    {
        if (transform.childCount == 0)
        {
            isSlotEmpty = true;
        }
        else
        {
            isSlotEmpty = false;
        }
        return isSlotEmpty;
    }

    public void CheckPlayableSlot()
    {
        if (changeEvent != null)
        {
            if (isSlotEmpty && playableCardSlot != null)
            {
                currentCard = null;
            }
            changeEvent(isSlotEmpty, currentCard);
        }
    }

    public void PutCardInSlot(Draggable d)
    {
        if (d != null)
        {
            if (transform.childCount > 0) //if there is another card in the slot
            {
                transform.GetChild(0).SetParent(d.startingParent);
            }

            d.transform.SetParent(transform);
            SetCard(d.card);
        }
    }

    private void SetCard(Card c)
    {
        currentCard = c;
        CheckPlayableSlot();
    }
}
