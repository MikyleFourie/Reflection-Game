using System.Collections;
using System.Collections.Generic;
using TMPro;  // Ensure you include this for TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Use TextMeshPro for dialogue text
    public TextMeshProUGUI speakerNameText;  // Use TextMeshPro for the speaker's name
    public GameObject dialoguePanel;  // The panel containing the dialogue UI
    public GameObject choicePanel;  // The panel containing choice UI
    public TextMeshProUGUI[] choiceTexts;  // Text elements for choices

    private Queue<string> dialogueLines;
    private bool isDialogueActive;  // Flag to check if dialogue is currently active
    private DialogueData currentDialogueData;  // Track current dialogue data

    // Property to check if dialogue is active
    public bool IsDialogueActive => isDialogueActive;

    private void Start()
    {
        dialoguePanel.SetActive(false);  // Hide the dialogue panel initially
        isDialogueActive = false;  // Ensure the dialogue is inactive at the start
        choicePanel.SetActive(false);  // Hide the choice panel initially
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        currentDialogueData = dialogueData;  // Store current dialogue data
        dialoguePanel.SetActive(true);
        speakerNameText.text = currentDialogueData.speakerName;

        dialogueLines = new Queue<string>(currentDialogueData.dialogueLines);  // Enqueue dialogue lines
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
            // If there are choices available, display them
            if (currentDialogueData.isBranching && currentDialogueData.choices.Count > 0)
            {
                DisplayChoices(); // Show the choices if they exist
                return;
            }

            EndDialogue();
            return;
        }

        string currentLine = dialogueLines.Dequeue();  // Get the next line
        StartCoroutine(DisplayLineCoroutine(currentLine));  // Start the coroutine for displaying the line
    }

    private void DisplayChoices()
    {
        choicePanel.SetActive(true);  // Show the choice panel
        for (int i = 0; i < currentDialogueData.choices.Count; i++)
        {
            choiceTexts[i].text = currentDialogueData.choices[i].choiceText;  // Display each choice
            int choiceIndex = i;  // Capture the index for the event handler
            choiceTexts[i].GetComponent<Button>().onClick.RemoveAllListeners(); // Clear existing listeners
            choiceTexts[i].GetComponent<Button>().onClick.AddListener(() => ChooseOption(choiceIndex)); // Add listener
        }
    }

    private void ChooseOption(int choiceIndex)
    {
        choicePanel.SetActive(false);  // Hide the choice panel
        StartDialogue(currentDialogueData.choices[choiceIndex].nextDialogue);  // Start the next dialogue
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
        choicePanel.SetActive(false);  // Ensure choice panel is hidden
    }
}
