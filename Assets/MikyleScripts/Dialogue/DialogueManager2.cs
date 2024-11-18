using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
    public GameObject player;
    public GameObject dialoguePanel; // Panel for the dialogue
    public TextMeshProUGUI DialogueText; // UI for dialogue
    public TextMeshProUGUI SpeakerText; // UI for speaker name
    public GameObject ChoicePanel; // Panel for player choices
    public Button ChoiceButtonPrefab; // Button template for choices
    public AudioSource AudioSource; // For voice clips

    private DialogueNode currentNode; // Active dialogue node
    private bool isTyping; // Prevents skipping during typewriter effect
    private bool waitingForInput;
    private EventManager eventManager;


    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
        player = GameObject.FindWithTag("Player");
        dialoguePanel.SetActive(false);  // Hide the dialogue panel initially
    }

    public void StartDialogue(DialogueNode startNode)
    {
        dialoguePanel.SetActive(true);  // Reveal the dialogue panel
        currentNode = startNode;

        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        if (currentNode == null)
        {
            Debug.Log("current node was null");
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
            Debug.Log("Options was 0");
            ChoicePanel.SetActive(false);
            //Invoke(nameof(ProcessEvents), AudioSource.clip?.length ?? 0f);
            waitingForInput = false;
        }
    }

    private void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            waitingForInput = false;
            ProcessEvents();
        }
    }

    private IEnumerator TypeText(string text)
    {
        Debug.Log("Typing...");
        isTyping = true;
        DialogueText.text = "";
        foreach (char c in text)
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(0.04f); // Typewriter speed
        }
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
        foreach (Transform child in ChoicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (DialogueOption option in currentNode.Options)
        {
            Button button = Instantiate(ChoiceButtonPrefab, ChoicePanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.Text;
            button.onClick.AddListener(() => OnChoiceSelected(option.NextNode));
        }
    }

    private void OnChoiceSelected(DialogueNode nextNode)
    {
        ChoicePanel.SetActive(false);
        currentNode = nextNode;
        DisplayDialogue();
    }

    private void ProcessEvents()
    {
        Debug.Log("Processing Events...");
        if (currentNode.Events != null)
        {
            foreach (DialogueEvent dialogueEvent in currentNode.Events)
            {
                dialogueEvent.Execute();
            }
        }

        if (currentNode.NextNode != null)
        {
            Debug.Log("Next node WAS NOT null");
            currentNode = currentNode.NextNode;
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        Debug.Log("Dialogue ended.");
        dialoguePanel.SetActive(false); // Hide the dialogue panel
        DialogueText.text = "";         // Clear the dialogue text
        SpeakerText.text = "";          // Clear the speaker name
        currentNode = null;             // Reset the current node
    }
}
