using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Set Up")]
    public bool isDroppedInSlot;
    public Transform startingParent;
    public GameObject cardName;
    public Card card;
    public CardSlot currentCardSlot;

    [Header("Monster Prefab")]
    public GameObject prefab;

    public static GameObject itemBeingDragged;

    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;
    private Vector3 cardPos;
    [SerializeField] private CardStats cs;

    private void Awake()
    {
        cs = GetComponent<CardStats>();
        cs.card = card;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardName.SetActive(false);
    }

    private void Update()
    {
        cardName.transform.position = Input.mousePosition;
        cardPos = cardName.transform.position;

        cardPos.y += 50f;

        cardName.transform.position = cardPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        originalPosition = transform.position;

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        isDroppedInSlot = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            currentCardSlot.PutCardInSlot(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.parent.name == "Inventory")
        {
            cardName.GetComponent<Description>().card = card;
            cardName.GetComponent<Description>().ShowName();
            cardName.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardName.GetComponent<Description>().card = null;
        cardName.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startingParent = transform.parent;
    }
}
