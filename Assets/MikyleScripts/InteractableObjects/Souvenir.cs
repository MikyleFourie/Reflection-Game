using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Souvenir : MonoBehaviour, IInteractable
{
    public string souvenirName;
    public bool isLoadable = false;
    public string nextLevel;

    public DialogueData souvenirDialogue;  // Reference to the dialogue data for sitting on the log
    private DialogueManager dialogueManager;  // Reference to the DialogueManager
    private SceneFader sceneFader;
    void Start()
    {
        // Automatically find and assign the DialogueManager in the scene
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene! Please ensure it is present.");
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
            StartCoroutine(HandleInteraction());
        }
        else
        {
            // If not loadable, just show the dialogue
            dialogueManager.StartDialogue(souvenirDialogue);
        }
    }

    private IEnumerator HandleInteraction()
    {
        // Start the dialogue
        dialogueManager.StartDialogue(souvenirDialogue);

        // Wait for the dialogue to complete
        yield return new WaitUntil(() => !dialogueManager.IsDialogueActive); // Use a property in DialogueManager to check if dialogue is still active

        //Fade Out
        if (sceneFader != null)
        {
            sceneFader.StartFadeOut();
        }
        else
        {
            Debug.LogError("SceneFader not found in the scene");
        }

        yield return new WaitForSeconds(3);

        // Load the next level after the dialogue is finished
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        Debug.Log("Loading next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel); // Load the next scene by name
    }

}

