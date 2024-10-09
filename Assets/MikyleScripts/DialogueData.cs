using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/BasicDialogue")]
public class DialogueData : ScriptableObject
{
    public string speakerName;  // The name of the speaker (NPC or player)

    [TextArea(3, 10)]  // Allows larger text areas in the inspector for dialogue lines
    public List<string> dialogueLines;  // List of dialogue lines for this interaction

    public bool isBranching;  // Flag for branching dialogue (if true, use choices)
    public List<DialogueChoice> choices;  // Optional: List of choices for branching dialogues
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;  // The text the player sees for the choice
    public DialogueData nextDialogue;  // The next dialogue that follows the player's choice
}

