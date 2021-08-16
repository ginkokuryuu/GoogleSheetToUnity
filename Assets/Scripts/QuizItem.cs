using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizItem : MonoBehaviour
{
    [SerializeField] string itemName = "placeholder";
    TMPro.TMP_Text textVisual;

    public string ItemName { get => itemName; }

    private void Awake()
    {
        textVisual = GetComponentInChildren<TMPro.TMP_Text>();
    }

    public void SetName(string _itemName)
    {
        itemName = _itemName;
        textVisual.text = itemName;
    }
}
