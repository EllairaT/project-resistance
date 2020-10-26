using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerExitHandler, IPointerEnterHandler
{
    #region variables
    [Header("Set Up")]
    public bool isDroppedInSlot;
    public Transform startingParent;
    public GameObject cardName;
    public Card card;
    public CardSlot currentCardSlot;
    public string inventoryName = "Inventory";
    public CardStats stats;

    [Header("Monster Prefab")]
    public GameObject prefab;

    public static GameObject itemBeingDragged;

    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;
    private Vector3 cardPos;
    private bool _over;
    #endregion

    private void Awake()
    {
        stats.card = card;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardName.SetActive(false);
    }

    private void Update()
    {
        if (_over)
        {
            cardName.transform.position = Input.mousePosition;
            cardPos = cardName.transform.position;

            cardPos.y += 50f;

            cardName.transform.position = cardPos;
        }
    }

    #region OnBeginDrag implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        originalPosition = transform.position;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        isDroppedInSlot = false;
    }
    #endregion

    #region OnDrag implementation
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    #endregion

    #region OnEndDrag implementation
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!isDroppedInSlot && transform.parent == startingParent)
        {
            transform.position = originalPosition;
        }
    }
    #endregion

    #region OnPointerClick implementation
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            currentCardSlot.PutCardInSlot(this);
        }
    }
    #endregion

    #region OnPointerEnter implementation
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.parent.name == inventoryName)
        {
            cardName.GetComponent<Description>().card = card;
            cardName.GetComponent<Description>().ShowName();
            cardName.SetActive(true);
        }
        _over = true;
    }
    #endregion

    #region OnPointerExit implementation
    public void OnPointerExit(PointerEventData eventData)
    {
        cardName.GetComponent<Description>().card = null;
        cardName.SetActive(false);
        _over = false;
    }
    #endregion

    #region OnPointerDown implementation
    public void OnPointerDown(PointerEventData eventData)
    {
        startingParent = transform.parent;
    }
    #endregion
}
