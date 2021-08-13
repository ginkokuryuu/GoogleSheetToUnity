using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    GameObject attachedObject;

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragAndDrop>().AttachToSlot(this);
        attachedObject = eventData.pointerDrag;
    }

    public virtual void DetachObject()
    {
        attachedObject = null;
    }
}
