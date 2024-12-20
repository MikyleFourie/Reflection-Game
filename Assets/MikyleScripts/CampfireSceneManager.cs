using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfireSceneManager : MonoBehaviour
{
    public DialogueManager2 dialogueManager2;
    public DialogueNode startingNode;

    //public DialogueManager dialogueManager; // Reference to your Dialogue Manager
    //public DialogueData introDialogue; // Reference to the introductory dialogue
    public Image fadePanel; // Assign the fade image from the Inspector
    public float fadeDuration = 3f; // Duration of the fade effect
    public Image crosshair;
    public GameObject player;
    private Color defaultColor;

    private void Start()
    {
        defaultColor = new Color(0, 0, 0, 222f / 255f);
        dialogueManager2 = FindObjectOfType<DialogueManager2>();
        crosshair.enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void BeginScene()
    {
        fadePanel.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Start with the overlay fully visible
        //fadePanel.color = Color.black;
        fadePanel.color = defaultColor;

        // Store the camera's initial rotation
        Quaternion initialRotation = Camera.main.transform.localRotation; // Assuming the main camera is being used
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0); // Target rotation

        // Gradually reduce the alpha value of the fade image to 0
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;

            // Linear fade effect
            fadePanel.color = Color.Lerp(defaultColor, Color.clear, normalizedTime);

            // Linear interpolation for camera rotation
            Camera.main.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, normalizedTime);

            yield return null; // Wait for the next frame
        }

        // Ensure the image is fully transparent at the end
        fadePanel.color = Color.clear;
        crosshair.enabled = true;
        //player.GetComponent<FirstPersonController>().enabled = true;

        // Call the method to show narration
        //StartDialogue();

        StartDialogue();
    }

    public void StartDialogue()
    {
        dialogueManager2.StartDialogue(startingNode);
    }
}
