using System.Collections;
using UnityEngine;

public class Alex : MonoBehaviour, IInteractable
{
    public DialogueData alexDialogue;  // Reference to Alex's dialogue data
    private DialogueManager dialogueManager;  // Reference to the DialogueManager

    private void Start()
    {
        // Automatically find and assign the DialogueManager in the scene
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene! Please ensure it is present.");
        }
    }

    public void Interact()
    {
        // Start the dialogue with Alex
        Debug.Log("Interacting with Alex.");
        dialogueManager.StartDialogue(alexDialogue);
    }
}
