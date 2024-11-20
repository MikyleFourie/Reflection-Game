using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
    public float dialogueSpeed = 0.04f;
    public GameObject player;
    public GameObject dialoguePanel; // Panel for the dialogue
    public TextMeshProUGUI DialogueText; // UI for dialogue
    public TextMeshProUGUI SpeakerText; // UI for speaker name
    public GameObject ChoicePanel; // Panel for player choices
    public Button[] choiceButtons; // Button array
    public AudioSource AudioSource; // For voice clips

    private DialogueNode currentNode; // Active dialogue node
    private bool isTyping; // Prevents skipping during typewriter effect
    private bool waitingForInput;
    private EventManager eventManager;


    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
        player = GameObject.FindWithTag("Player");
        AudioSource = player.GetComponent<AudioSource>();

        choiceButtons = ChoicePanel.GetComponentsInChildren<Button>();
        ChoicePanel.SetActive(false);

        dialoguePanel.SetActive(false);  // Hide the dialogue panel initially
    }

    public void StartDialogue(DialogueNode startNode)
    {
        ChoicePanel.SetActive(false);
        dialoguePanel.SetActive(true);  // Reveal the dialogue panel
        currentNode = startNode;

        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        if (currentNode == null)
        {
            //Debug.Log("current node was null");
            EndDialogue();
            return;
        }

        Debug.Log("Node: " + currentNode.NodeID);

        //Update speaker and start typewriter effect
        SpeakerText.text = currentNode.Speaker;
        StopAllCoroutines();
        StartCoroutine(TypeText(currentNode.DialogueText));
        PlayAudio(currentNode.AudioClip);

        //Handle Options
        if (currentNode.Options.Length > 0)
        {
            DisplayOptions();
        }
        else
        {
            ChoicePanel.SetActive(false);
            //waitingForInput = false;
        }
    }

    private void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space) && !isTyping && currentNode.Options.Length <= 0)
        {
            waitingForInput = false;
            ProcessEvents();
        }
    }

    private IEnumerator TypeText(string text)
    {
        // Debug.Log("Typing...");
        isTyping = true;
        DialogueText.text = "";
        foreach (char c in text)
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(dialogueSpeed); // Typewriter speed
        }
        choiceButtons[0].Select();
        isTyping = false;
        waitingForInput = true;


    }

    private void PlayAudio(AudioClip clip)
    {
        if (clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }
    }

    private void DisplayOptions()
    {
        ChoicePanel.SetActive(true);
        for (int i = 0; i < currentNode.Options.Length; i++)
        {
            int optionIndex = i; // Capture the index by value

            // Update the button's text
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.Options[optionIndex].Text;

            // Clear previous listeners to avoid stacking
            choiceButtons[i].onClick.RemoveAllListeners();

            // Add the new listener
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(currentNode.Options[optionIndex].NextNode));
        }

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
        //choiceButtons[0].Select(); // Set the first button as the selected one


    }

    private void OnChoiceSelected(DialogueNode nextNode)
    {
        waitingForInput = true;
        //choiceButtons[0].Select(); // Set the first button as the selected one
        ChoicePanel.SetActive(false);
        currentNode = nextNode;
        DisplayDialogue();
    }

    private void ProcessEvents()
    {
        // Debug.Log("Processing Events...");
        if (currentNode.Events != null && currentNode.Events.Count() > 0)
        {
            eventManager.ProcessEvents(currentNode.Events, OnEventsComplete);
        }
        else
        {
            OnEventsComplete();
        }

        //if (currentNode.NextNode != null)
        //{
        //    Debug.Log("Next node WAS NOT null");
        //    currentNode = currentNode.NextNode;
        //    DisplayDialogue();
        //}
        //else
        //{
        //    EndDialogue();
        //}
    }

    private void OnEventsComplete()
    {
        if (currentNode.RequiresInteraction)
        {
            //  Debug.Log("Waiting for player interaction...");
            return; // Wait until external trigger moves to the next node
        }

        if (currentNode.NextNode != null)
        {
            currentNode = currentNode.NextNode;
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void OnPlayerInteraction()
    {
        if (currentNode != null && currentNode.RequiresInteraction)
        {
            currentNode = currentNode.NextNode;
            DisplayDialogue();
        }
    }

    private void EndDialogue()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        // Debug.Log("Dialogue ended.");
        ChoicePanel.SetActive(false);
        dialoguePanel.SetActive(false); // Hide the dialogue panel
        DialogueText.text = "";         // Clear the dialogue text
        SpeakerText.text = "";          // Clear the speaker name
        currentNode = null;             // Reset the current node
    }
}
