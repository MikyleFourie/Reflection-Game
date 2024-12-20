using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Log : MonoBehaviour, IInteractable
{
    public bool isSitting = false; // Track whether the player is sitting
    public GameObject player;
    public GameObject marker;
    private FirstPersonController firstPersonController;
    private Vector3 logForward;
    private GameObject fireLight;

    //public DialogueData sittingDialogue;  // Reference to the dialogue data for sitting on the log
    public DialogueNode startNode;
    //private DialogueManager dialogueManager;  // Reference to the DialogueManager
    private DialogueManager2 dialogueManager2;  // Reference to the DialogueManager

    void Start()
    {
        fireLight = transform.parent.Find("FireLight")?.gameObject;
        //AlignToFireLight(fireLight);
        marker = this.transform.GetChild(0).gameObject; // Get the marker (child object)
        player = FirstPersonController.Instance.gameObject; // Reference to the player
        firstPersonController = player.GetComponent<FirstPersonController>(); // Get the FirstPersonController component
        logForward = transform.right; // Local X-axis direction of the log

        // Automatically find and assign the DialogueManager in the scene
        //dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager2 = FindObjectOfType<DialogueManager2>();

        if (dialogueManager2 == null)
        {
            Debug.LogError("DialogueManager not found in the scene! Please ensure it is present.");
        }

    }

    public void Interact()
    {
        // Call sit/stand functionality
        if (isSitting)
        {
            StandUp();
        }
        else
        {
            SitDown();
        }
    }

    private void SitDown()
    {
        Debug.Log("SitDown in Log ran");
        firstPersonController.SetCanMove(false); //Disable Movement
        firstPersonController.SetSittingLog(this); // Set this log as the current one
        isSitting = true; // Update sitting state

        // Move the player to the marker's position
        player.transform.position = marker.transform.position; // Move to marker position
        player.transform.rotation = Quaternion.LookRotation(logForward); // Align player to face log's local X-axis



        Debug.Log("sitting func should start");

        // Start the dialogue when sitting down
        if (dialogueManager2 != null && startNode != null)
        {
            Debug.Log("sitting dialogue should start");
            //dialogueManager.StartDialogue(sittingDialogue); // Start the dialogue
            dialogueManager2.StartDialogue(startNode);
        }
    }

    private void StandUp()
    {
        Debug.Log("StandUp in Log ran");

        firstPersonController.SetCanMove(true); //Disable Movement
        isSitting = false; // Update sitting state
        Debug.Log("Player is standing up from the log.");
    }

    public void PlayerStoodUp()
    {
        Debug.Log("Player Stood Up Ran");

        isSitting = false; // Update sitting state when notified by the FirstPersonController
        Debug.Log("Log updated: player is no longer sitting.");
    }

    // Method to align the log to face the FireLight
    private void AlignToFireLight(GameObject fireLight)
    {
        // Calculate the direction to the FireLight
        Vector3 directionToFireLight = fireLight.transform.position - transform.position;

        // Set the Y value of the direction to 0 to ignore pitch (X-axis rotation)
        directionToFireLight.y = 0;

        // Normalize the direction vector
        directionToFireLight.Normalize();

        // Calculate the new Y-axis rotation using the direction vector
        // Inverting the Z component if necessary to correct alignment
        float newYRotation = Mathf.Atan2(directionToFireLight.x, directionToFireLight.z) * Mathf.Rad2Deg;

        // Create a new rotation that keeps the current X and Z rotation
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, newYRotation, transform.eulerAngles.z);

        // Apply the new rotation to the log
        transform.rotation = newRotation;

        // Log the rotation for debugging
        Debug.Log("Log's New Rotation: " + transform.rotation.eulerAngles);
    }
}