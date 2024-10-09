using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Souvenir : MonoBehaviour, IInteractable
{
    public string souvenirName;
    public bool isLoadable = false;
    public string nextLevel;

    public void Interact()
    {
        // Code to display the souvenir description when interacted with
        Debug.Log("Interacting with souvenir: " + souvenirName);

        // If this is the first souvenir, load the next level
        if (isLoadable && !string.IsNullOrEmpty(nextLevel))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        Debug.Log("Loading next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel); // Load the next scene by name
    }
}

