using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic : MonoBehaviour, IInteractable
{
    public DialogueNode dialogueNode;
    private DialogueManager2 dialogueManager2;
    void Start()
    {
        // Automatically find and assign the DialogueManager in the scene
        dialogueManager2 = FindObjectOfType<DialogueManager2>();

        if (dialogueManager2 == null)
        {
            Debug.LogError("DialogueManager2 not found in the scene! Please ensure it is present.");
        }
    }
    public void Interact()
    {
        // Code to display the souvenir description when interacted with
        Debug.Log("Interacting with souvenir: " + transform.name);

        dialogueManager2.StartDialogue(dialogueNode);

    }


}
