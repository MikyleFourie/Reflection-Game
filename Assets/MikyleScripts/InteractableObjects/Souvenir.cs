using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Souvenir : MonoBehaviour, IInteractable
{
    public string souvenirName;
    public bool isLoadable = false;
    public string nextLevel;

    //public DialogueData souvenirDialogue;  // Reference to the dialogue data for sitting on the log
    //DialogueManager dialogueManager;  // Reference to the DialogueManager

    public DialogueNode souvenirNode;
    private DialogueManager2 dialogueManager2;
    private SceneFader sceneFader;
    void Start()
    {
        // Automatically find and assign the DialogueManager in the scene
        dialogueManager2 = FindObjectOfType<DialogueManager2>();

        if (dialogueManager2 == null)
        {
            Debug.LogError("DialogueManager2 not found in the scene! Please ensure it is present.");
        }

        // Find the SceneFader in the scene
        sceneFader = FindObjectOfType<SceneFader>();


    }
    public void Interact()
    {
        // Code to display the souvenir description when interacted with
        Debug.Log("Interacting with souvenir: " + souvenirName);

        // Start the dialogue and wait for it to finish if it's loadable
        if (isLoadable && !string.IsNullOrEmpty(nextLevel))
        {
            dialogueManager2.StartDialogue(souvenirNode);
        }
        else
        {
            // If not loadable, just show the dialogue
            dialogueManager2.StartDialogue(souvenirNode);
        }
    }

    private IEnumerator HandleInteraction()
    {

        dialogueManager2.StartDialogue(souvenirNode);

        yield break;

    }

    private void LoadNextLevel()
    {
        Debug.Log("Loading next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel); // Load the next scene by name
    }

}

