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

    void Start()
    {
        fireLight = transform.parent.Find("FireLight")?.gameObject;
        //AlignToFireLight(fireLight);
        marker = this.transform.GetChild(0).gameObject; // Get the marker (child object)
        player = FirstPersonController.Instance.gameObject; // Reference to the player
        firstPersonController = player.GetComponent<FirstPersonController>(); // Get the FirstPersonController component
        logForward = transform.right; // Local X-axis direction of the log
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

        // Move the player to the marker's position
        player.transform.position = marker.transform.position; // Move to marker position
        player.transform.rotation = Quaternion.LookRotation(logForward); // Align player to face log's local X-axis

        firstPersonController.SetCanMove(false); //Disable Movement
        firstPersonController.SetSittingLog(this); // Set this log as the current one
        isSitting = true; // Update sitting state
    }

    private void StandUp()
    {
        firstPersonController.SetCanMove(true); //Disable Movement
        isSitting = false; // Update sitting state
        Debug.Log("Player is standing up from the log.");
    }

    public void PlayerStoodUp()
    {
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