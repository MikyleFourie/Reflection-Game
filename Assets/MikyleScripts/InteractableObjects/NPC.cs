using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    //public string npcName;
    //public DialogueData npcDialogue;
    //private DialogueManager dialogueManager;

    //void Start()
    //{
    //    // Automatically find and assign the DialogueManager in the scene
    //    dialogueManager = FindObjectOfType<DialogueManager>();

    //    if (dialogueManager == null)
    //    {
    //        Debug.LogError("DialogueManager not found in the scene! Please ensure it is present.");
    //    }
    //}
    public void Interact()
    {
        //    // Code to start dialogue when interacting with NPC
        //    Debug.Log("Interacting with NPC: " + npcName);
        //    dialogueManager.StartDialogue(npcDialogue);
    }
}

