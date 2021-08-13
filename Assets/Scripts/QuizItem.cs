using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizItem : MonoBehaviour
{
    [SerializeField] string itemName = "";

    public string ItemName { get => itemName; set => itemName = value; }
}
