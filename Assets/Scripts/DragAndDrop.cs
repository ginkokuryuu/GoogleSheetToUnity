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
    bool initPosAlreadySet = false;
    DragSlot dragSlot;
    Transform formerParent;

    public int maximumNumerOfTryingToFindCanvas = 5;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.position;
    }

    public void SetInitPosition()
    {
        initPosAlreadySet = true;
        initialPosition = rectTransform.position;
    }

    void GetFormerParent()
    {
        formerParent = transform.parent;
    }

    void TryToSearchCanvas()
    {
        int count = 0;
        Transform parent = transform;
        while (canvas == null)
        {
            parent = parent.parent;
            if(parent.GetComponent<Canvas>() != null)
                canvas = parent.GetComponent<Canvas>();
            count += 1;
            if (count == maximumNumerOfTryingToFindCanvas)
            {
                Debug.LogError("No Canvas Found");
                break;
            }
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
        if (canvas == null)
        {
            TryToSearchCanvas();
        }

        if (!initPosAlreadySet)
        {
            SetInitPosition();
        }

        if(formerParent == null)
        {
            GetFormerParent();
        }
    }

    public void AttachToSlot(DragSlot slot)
    {
        dragSlot = slot;
    }

    public void ResetPosition()
    {
        rectTransform.position = initialPosition;
    }
}
