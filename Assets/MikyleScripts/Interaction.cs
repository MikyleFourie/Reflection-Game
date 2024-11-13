using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public float interactionDistance; // Max distance for interaction
    public LayerMask interactableLayer = default; // Layer to identify interactable objects
    public GameObject descriptionPanel; // Reference to the description UI panel
    public GameObject aimPanel; // Crosshair/aim panel
    public FirstPersonController firstPersonController; // Reference to first-person controller script
    public TMPro.TMP_Text descriptionText; // Text component for displaying descriptions

    public IInteractable currentInteractable; // Cache the current interactable object
    public string currentInteractableName;

    void Update()
    {
        //HandleCloseDescription();

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
            //Debug.Log("item: " + hit.transform.name);
            if (interactable != null)
            {
                currentInteractable = interactable; // Cache the interactable object
                currentInteractableName = hit.transform.name;
                Debug.Log("currentInteractable: " + currentInteractable);

            }
            else
            {
                currentInteractableName = "";
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractableName = "";
            currentInteractable = null; // Clear interactable when nothing is hit
        }
    }





}
