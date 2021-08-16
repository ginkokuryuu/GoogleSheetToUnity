using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuizSlot : DragSlot
{
    [SerializeField] string slotName = "";
    bool isCorrect = false;

    public bool IsCorrect { get => isCorrect; set => isCorrect = value; }

    private void Start()
    {
        QuizHandler.INSTANCE.AllQuizSlots.Add(this);
    }

    private void OnDisable()
    {
        QuizHandler.INSTANCE.AllQuizSlots.Remove(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (attachedObject != null)
            return;

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
