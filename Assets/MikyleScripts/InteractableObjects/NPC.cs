using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string npcName;

    public void Interact()
    {
        // Code to start dialogue when interacting with NPC
        Debug.Log("Interacting with NPC: " + npcName);
    }
}

