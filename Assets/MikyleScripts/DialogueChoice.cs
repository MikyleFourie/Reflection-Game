using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;          // What the player sees as a choice
    public DialogueLine nextLine;      // What dialogue follows this choice
}

[System.Serializable]
public class BranchingDialogue : DialogueLine
{
    public List<DialogueChoice> choices; // List of player choices leading to different branches
}

