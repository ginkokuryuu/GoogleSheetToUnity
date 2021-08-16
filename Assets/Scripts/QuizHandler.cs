using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler INSTANCE;

    [SerializeField] GameObject quizContainer = null;
    [SerializeField] GameObject quizItemPrefab = null;
    [SerializeField] GameObject quizSlotPrefab = null;
    [SerializeField] GameObject pageContainerPrefab = null;
    [SerializeField] GameObject quizSlotContainer = null;
    [SerializeField] GameObject quizItemContainer = null;
    [SerializeField] List<QuizData> allQuestions = new List<QuizData>();
    [SerializeField] int questionPerPage = 5;
    List<QuizSlot> allQuizSlots = new List<QuizSlot>();
    int currentPage = 0;
    int questionCount = 0;
    int pageNeeded = 0;
    bool isGeneratingQuiz = false;
    int pageReady = 0;

    public List<QuizSlot> AllQuizSlots { get => allQuizSlots; set => allQuizSlots = value; }

    private void Awake()
    {
        if (INSTANCE == null)
            INSTANCE = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        GeneratePagedQuiz();
    }

    public void GeneratePagedQuiz()
    {
        questionCount = allQuestions.Count;
        pageNeeded = (int)Mathf.Ceil((float)questionCount / (float)questionPerPage);

        isGeneratingQuiz = true;

        for (int page = 0; page < pageNeeded; page++)
        {
            GameObject pageContainer = Instantiate(pageContainerPrefab, quizContainer.transform);
            Transform _quizSlotContainer = pageContainer.transform.GetChild(0);
            Transform _quizItemContainer = pageContainer.transform.GetChild(1);
            for (int i = 0; i < questionPerPage; i++)
            {
                if (allQuestions.Count == 0)
                    break;

                int index = Random.Range(0, allQuestions.Count);

                GameObject quizSlot = Instantiate(quizSlotPrefab, _quizSlotContainer.transform);
                quizSlot.GetComponent<QuizSlot>().SetName(allQuestions[index].componentName);

                GameObject quizItem = Instantiate(quizItemPrefab, _quizItemContainer.transform);
                quizItem.GetComponent<QuizItem>().SetName(allQuestions[index].componentName);

                allQuestions.RemoveAt(index);
            }

            StartCoroutine(DisableGroupLayout(_quizItemContainer.gameObject));
        }
    }

    IEnumerator DisableGroupLayout(GameObject _quizItemContainer)
    {
        yield return 1;

        _quizItemContainer.GetComponent<VerticalLayoutGroup>().enabled = false;

        pageReady += 1;
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

        if (Input.GetKeyDown(KeyCode.L))
            NextPage();
        else if (Input.GetKeyDown(KeyCode.K))
            PreviousPage();

        if (isGeneratingQuiz)
        {
            if(pageReady == pageNeeded)
            {
                isGeneratingQuiz = false;
                PresentFirstPage();
            }
        }
    }

    void PresentFirstPage()
    {
        for(int i = 1; i < quizContainer.transform.childCount; i++)
        {
            quizContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPage == pageNeeded - 1)
            return;

        quizContainer.transform.GetChild(currentPage).gameObject.SetActive(false);

        currentPage += 1;

        quizContainer.transform.GetChild(currentPage).gameObject.SetActive(true);
    }

    public void PreviousPage()
    {
        if (currentPage == 0)
            return;

        quizContainer.transform.GetChild(currentPage).gameObject.SetActive(false);

        currentPage -= 1;

        quizContainer.transform.GetChild(currentPage).gameObject.SetActive(true);
    }
}
