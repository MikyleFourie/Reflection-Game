using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public bool fadeInOnStart = true;
    public Image fadePanel; // Assign the fade image from the Inspector
    public float fadeDuration = 3f; // Duration of the fade effect
    public Image crosshair;
    public GameObject player;

    private void Start()
    {
        crosshair.enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        if (fadeInOnStart)
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

    private IEnumerator FadeOut()
    {
        // Start with the overlay fully transparent
        fadePanel.color = Color.clear;

        // Gradually increase the alpha value of the fade image to 1 (black)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;

            // Exponential easing: accelerate the darkening effect
            float easedTime = Mathf.Pow(normalizedTime, 2); // This creates an exponential effect

            fadePanel.color = Color.Lerp(Color.clear, Color.black, easedTime);
            yield return null; // Wait for the next frame
        }

        // Ensure the image is fully opaque at the end
        fadePanel.color = Color.black;

        // Optionally, disable player movement or signal the start of a new scene
        crosshair.enabled = false; // Hide crosshair if needed
        player.GetComponent<FirstPersonController>().enabled = false; // Disable movement or other actions
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
