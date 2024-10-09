using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image fadePanel; // Assign the fade image from the Inspector
    public float fadeDuration = 3f; // Duration of the fade effect
    public Image crosshair;
    public GameObject player;

    private void Start()
    {
        crosshair.enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Start with the overlay fully visible
        fadePanel.color = Color.black;

        // Gradually reduce the alpha value of the fade image to 0
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;

            // Exponential easing: accelerate the clearing effect
            float easedTime = Mathf.Pow(normalizedTime, 2); // This creates an exponential effect

            fadePanel.color = Color.Lerp(Color.black, Color.clear, easedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure the image is fully transparent at the end
        fadePanel.color = Color.clear;
        crosshair.enabled = true;
        player.GetComponent<FirstPersonController>().enabled = true;

        // Allow player movement here or signal that the game can proceed
    }
}
