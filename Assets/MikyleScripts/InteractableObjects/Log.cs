using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Log : MonoBehaviour, IInteractable
{
    public Vector3 sitPosition; // Position where the player will sit
    public Vector3 sitLocalPosition; // Position where the player will sit
    public Quaternion sitRotation; // Rotation where the player will sit
    private bool isSitting = false; // Track whether the player is sitting
    public GameObject player;

    void Start()
    {
        player = FirstPersonController.Instance.gameObject;
        sitPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        sitLocalPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);


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
        sitRotation = player.transform.rotation;
        sitRotation.y = this.transform.localEulerAngles.y;
        Debug.Log(sitRotation.y);
        Debug.Log(this.transform.localEulerAngles.y);
        // Move the player to the sitting position
        // Assuming you have a reference to the player in your Interaction script or globally
        player.transform.position = sitLocalPosition; // Move to sit position
        player.transform.rotation = sitRotation;
        //FirstPersonController.Instance.transform.rotation = sitPosition.rotation; // Optional: Match rotation
        isSitting = true; // Update sitting state
        Debug.Log("Player is sitting down on the log.");
    }

    private void StandUp()
    {
        // Move the player back to their original position (you may need to store the original position)
        //FirstPersonController.Instance.transform.position += Vector3.up; // Adjust based on your game
        isSitting = false; // Update sitting state
        Debug.Log("Player is standing up from the log.");
    }
}

