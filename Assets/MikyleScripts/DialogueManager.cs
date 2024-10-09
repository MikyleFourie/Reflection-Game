using System.Collections;
using System.Collections.Generic;
using TMPro;  // Ensure you include this for TextMeshPro
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Use TextMeshPro for dialogue text
    public TextMeshProUGUI speakerNameText;  // Use TextMeshPro for the speaker's name
    public GameObject dialoguePanel;  // The panel containing the dialogue UI

    private Queue<string> dialogueLines;
    private bool isDialogueActive;  // Flag to check if dialogue is currently active

    // Property to check if dialogue is active
    public bool IsDialogueActive => isDialogueActive;

    private void Start()
    {
        dialoguePanel.SetActive(false);  // Hide the dialogue panel initially
        isDialogueActive = false;  // Ensure the dialogue is inactive at the start
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        dialoguePanel.SetActive(true);
        speakerNameText.text = dialogueData.speakerName;

        dialogueLines = new Queue<string>(dialogueData.dialogueLines);  // Enqueue dialogue lines
        isDialogueActive = true; // Set flag to true when dialogue starts
        DisplayNextLine();
    }

    private IEnumerator DisplayLineCoroutine(string line)
    {
        dialogueText.text = line;  // Display the current line
        yield return new WaitForSeconds(4);  // Wait for 4 seconds (or desired duration)
        DisplayNextLine();  // Automatically display the next line
    }

    public void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string currentLine = dialogueLines.Dequeue();  // Get the next line
        StartCoroutine(DisplayLineCoroutine(currentLine));  // Start the coroutine for displaying the line
    }

    private void Update()
    {
        // Check for input to progress the dialogue
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))  // Change KeyCode as needed
        {
            DisplayNextLine();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false; // Set flag to false when dialogue ends
        dialoguePanel.SetActive(false);
    }
}
