using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 originalPosition;
    public bool isDroppedInSlot;
    public Transform startingParent;

    public static GameObject itemBeingDragged; //will ensure the user will only be able to drag one item at a time


    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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

    public void OnPointerDown(PointerEventData eventData) {}
}
