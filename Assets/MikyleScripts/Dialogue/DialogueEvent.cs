using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class DialogueEvent : ScriptableObject
{
    // Abstract method that derived classes must implement
    public abstract void Execute();
}
