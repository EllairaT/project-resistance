﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 originalPosition;
    public bool isDroppedInSlot;
    public Transform startingParent;
    public GameObject cardName;

    public Card card;
    //TODO check if parent is inventory
    public static GameObject itemBeingDragged; //will ensure the user will only be able to drag one item at a time
    private CanvasGroup canvasGroup;
    Vector3 cardPos;
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
        startingParent = transform.parent;  //its parent is the current slot

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

    public void OnPointerDown(PointerEventData eventData)
    {

        if(eventData.clickCount == 2)
        {
            Debug.Log("double clicked: " + card.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardName.GetComponent<Description>().card = card;
        cardName.GetComponent<Description>().ShowName();
        cardName.SetActive(true);
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardName.GetComponent<Description>().card = null;
        cardName.SetActive(false);
    }

    private void ToggleDescription()
    {

    }
}
