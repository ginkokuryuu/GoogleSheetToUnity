using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    Vector2 initialPosition = new Vector2();
    DragSlot dragSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

        while (canvas == null)
        {
            Transform parent = transform.parent;
            canvas = parent.GetComponent<Canvas>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        dragSlot?.DetachObject();
        dragSlot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (dragSlot == null)
            ResetPosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void AttachToSlot(DragSlot slot)
    {
        dragSlot = slot;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initialPosition;
    }
}
