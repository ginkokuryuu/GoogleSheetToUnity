using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question Data", menuName = "Question Data")]
public class QuizData : ScriptableObject
{
    public string componentName = "temporary name";
    public Vector2 componentPosition = new Vector2();
}
