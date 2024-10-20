using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 40f; // Max distance for interaction
    public LayerMask interactableLayer = default; // Layer to identify interactable objects
    public GameObject descriptionPanel; // Reference to the description UI panel
    public GameObject aimPanel; // Crosshair/aim panel
    public FirstPersonController firstPersonController; // Reference to first-person controller script
    public TMPro.TMP_Text descriptionText; // Text component for displaying descriptions

    public IInteractable currentInteractable; // Cache the current interactable object

    void Update()
    {
        HandleCloseDescription();
        CheckForInteractable();

        // Handle interaction when the left mouse button is pressed
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.Interact(); // Call the interact method on the object
        }
    }

    // Method to handle hiding the description panel
    void HandleCloseDescription()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            descriptionPanel.SetActive(false); // Hide the description panel
            aimPanel.SetActive(true); // Show the aim panel
            firstPersonController.enabled = true; // Enable player movement
        }
    }

    // Method to raycast and check for interactable objects
    void CheckForInteractable()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Perform raycast to detect interactable objects
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            var interactable = hit.transform.GetComponent<IInteractable>();
            Debug.Log("item: " + hit.transform.name);
            if (interactable != null)
            {
                currentInteractable = interactable; // Cache the interactable object
                HighlightObject(hit.transform.gameObject); // Highlight the object if necessary
                Debug.Log("currentInteractable: " + currentInteractable);
            }
            else
            {
                currentInteractable = null;
                RemoveHighlight(hit.transform.gameObject); // Remove highlight when not looking at an interactable
            }
        }
        else
        {
            currentInteractable = null; // Clear interactable when nothing is hit
        }
    }

    // Example method to handle object highlighting
    void HighlightObject(GameObject obj)
    {
        // Implement highlight logic, like changing material or adding an outline
    }

    void RemoveHighlight(GameObject obj)
    {
        // Remove highlight logic, restoring original state
    }


}
