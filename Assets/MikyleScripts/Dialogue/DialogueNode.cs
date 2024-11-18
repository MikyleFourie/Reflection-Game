using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    public string NodeID; // Unique identifier
    public string Speaker; // Who's speaking
    [TextArea] public string DialogueText; // Dialogue text
    public AudioClip AudioClip; // Optional voice clip
    public DialogueOption[] Options; // Player choices (optional)
    public DialogueNode NextNode; // Next node (if no choices)
    public DialogueEvent[] Events; // Events tied to this node
}

[System.Serializable]
public class DialogueOption
{
    public string Text; // Text for the choice
    public DialogueNode NextNode; // Next node to trigger
}

//[System.Serializable]
//public class DialogueEvent
//{
//    public enum EventType { PauseDialogue, TriggerInteraction, PlayAnimation }
//    public EventType Type;
//    public string Target; // Object or parameter
//    public string Action; // Specific action (e.g., "PickUp")
//}
