using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souvenir : MonoBehaviour, IInteractable
{
    public string souvenirName;

    public void Interact()
    {
        // Code to display the souvenir description when interacted with
        Debug.Log("Interacting with souvenir: " + souvenirName);
    }
}

