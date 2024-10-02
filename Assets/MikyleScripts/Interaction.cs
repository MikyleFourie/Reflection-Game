using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 40f; // Max distance for interaction
    public LayerMask souvenirLayer = default; // Layer to identify souvenir objects
    public GameObject descriptionPanel; // Reference to the description UI panel
    public GameObject aimPanel; // Crosshair/aim panel
    public FirstPersonController firstPersonController; // Reference to first-person controller script

    public TMPro.TMP_Text descriptionText; // Text component for displaying souvenir description

    // Dictionary or external data source for descriptions
    private Dictionary<string, string> souvenirDescriptions = new Dictionary<string, string>
    {
        { "Souvenir (1)", "Hmm... This is the Souvenir I got when I met [Character A]." },
        { "Souvenir (2)", "Hmm... This is the Souvenir I got when I met [Character B]." },
        { "Souvenir (3)", "Hmm... This is the Souvenir I got when I met [Character C]." },
        { "Souvenir (4)", "Hmm... This is the Souvenir I got when I met [Character D]." },
        { "Souvenir (5)", "Hmm... This is the Souvenir I got when I met [Character E]." },
        // Add more souvenir descriptions as needed
    };

    void Update()
    {
        // Handle hiding the description panel with 'E', 'Escape', or right-click
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            descriptionPanel.SetActive(false); // Hide the description panel
            aimPanel.SetActive(true); // Show the aim panel
            firstPersonController.enabled = true; // Enable player movement
        }

        // Handle left mouse click for interaction
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.Log("Ray OUT!");

            // Raycast from the camera to detect objects
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, souvenirLayer))
            {
                // Debug line to log the name of the object hit
                Debug.Log("Hit object: " + hit.transform.name);

                // Check if the clicked object is a souvenir
                if (hit.transform.CompareTag("Souvenir"))
                {
                    // Call the method to handle showing the description
                    ShowSouvenirDescription(hit.transform.gameObject);
                }
            }
        }
    }

    // Method to handle displaying the souvenir description
    void ShowSouvenirDescription(GameObject souvenir)
    {
        // Disable player movement and show description UI
        firstPersonController.enabled = false;
        aimPanel.SetActive(false);
        descriptionPanel.SetActive(true);

        // Get the name of the souvenir to fetch the corresponding description
        string souvenirName = souvenir.name;

        // Check if there's a description for this souvenir
        if (souvenirDescriptions.ContainsKey(souvenirName))
        {
            // Display the description in the text component
            descriptionText.text = souvenirDescriptions[souvenirName];
        }
        else
        {
            // Display a default message if no description is found
            descriptionText.text = "No description available for this souvenir.";
        }
    }
}
