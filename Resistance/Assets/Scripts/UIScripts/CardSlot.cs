using UnityEngine;
using UnityEngine.EventSystems;


public class CardSlot : BaseMonobehaviour, IDropHandler
{
    public Card currentCard;
    public CardStats currentCardStats;

    public delegate void ChangeEvent(bool slotState, Card c, CardStats cs);
    public static event ChangeEvent changeEvent;
    public bool isSlotEmpty = true;

    // "playable card" refers to the card in the slot on the lefthand side of the screen.
    public bool isPlayableCardSlot;

    #region DropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null && eventData.pointerDrag != null)
        {
            CheckSlot(eventData.pointerDrag.GetComponent<Draggable>().startingParent);
            Draggable.itemBeingDragged.transform.SetParent(transform); //set this slot as card's parent.

            SetAll(eventData.pointerDrag.GetComponent<Draggable>().card,
                   eventData.pointerDrag.GetComponent<Draggable>().stats);
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

    //function to check if the playable slot is empty
    public void CheckPlayableSlot()
    {
        if (changeEvent != null)
        {
            if (isSlotEmpty && isPlayableCardSlot)
            {
                currentCard = null;
                currentCardStats = null;
            }
            //trigger an event and tell the other classes subscribed to the event that something was changed
            changeEvent(isSlotEmpty, currentCard, currentCardStats);
        }
    }

    //this is called when user double clicks the card instead of dragging it
    public void PutCardInSlot(Draggable d)
    {
        if (d != null)
        {
            CheckSlot(d.startingParent);
            d.transform.SetParent(transform);
            SetAll(d.card, d.stats);
        }
    }

    //check if there is another card in the slot
    private void CheckSlot(Transform parent)
    {
        //if so, then set the current card's parent as that card's parent.
        if (transform.childCount > 0)
        {        
            transform.GetChild(0).SetParent(parent);
        }
    }

    private void SetCard(Card c)
    {
        currentCard = c;
    }

    private void SetCardStats(CardStats cs)
    {
        currentCardStats = cs;
    }

    private void SetAll(Card c, CardStats cs)
    {
        SetCard(c);
        SetCardStats(cs);
        CheckPlayableSlot();
    }
}
