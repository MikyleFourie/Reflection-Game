using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class DialogueEvent : ScriptableObject
{
    public float Duration = 0f; //Optional Duration in seconds
    // Abstract method that derived classes must implement
    public abstract void Execute(float Duration, System.Action onComplete);
}
