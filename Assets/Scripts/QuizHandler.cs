using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler INSTANCE;

    List<QuizSlot> allQuizSlots = new List<QuizSlot>();
    public List<QuizSlot> AllQuizSlots { get => allQuizSlots; set => allQuizSlots = value; }

    private void Awake()
    {
        if (INSTANCE == null)
            INSTANCE = this;
        else
            Destroy(this.gameObject);
    }

    public void CheckQuiz()
    {
        int score = 0;
        int maxScore = allQuizSlots.Count;

        foreach(QuizSlot slot in allQuizSlots)
        {
            if (slot.IsCorrect)
                score += 1;
        }

        Debug.Log("Score : " + score + "/" + maxScore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            CheckQuiz();
    }
}
