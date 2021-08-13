using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuizSlot : DragSlot
{
    [SerializeField] string slotName = "";
    bool isCorrect = false;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        QuizItem quizItem = eventData.pointerDrag.GetComponent<QuizItem>();
        if (quizItem == null)
            return;

        MatchName(quizItem.ItemName);

        base.OnDrop(eventData);
    }

    void MatchName(string itemName)
    {
        if (itemName.CompareTo(slotName) == 0)
            isCorrect = true;
        else
            isCorrect = false;
    }

    public override void DetachObject()
    {
        base.DetachObject();

        isCorrect = false;
    }
}
