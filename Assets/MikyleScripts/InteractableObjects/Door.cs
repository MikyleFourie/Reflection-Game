using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public GameObject marker1;
    public GameObject marker2;
    GameObject player;
    public bool isInside = false;
    public GameObject otherDoor;
    Door otherDoorSc;


    public void Interact()
    {
        player = GameObject.FindWithTag("Player");
        otherDoorSc = otherDoor.GetComponent<Door>();

        // Temporarily disable the CharacterController
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        if (!isInside)
        {
            isInside = true;
            otherDoorSc.isInside = true;

            //Rotate door and other door
            Vector3 center = this.GetComponent<Renderer>().bounds.center;
            this.transform.RotateAround(center, Vector3.up, 180f); // Rotate 180 degrees around the Y-axis
            //repeat for other door
            center = otherDoor.GetComponent<Renderer>().bounds.center;
            otherDoor.transform.RotateAround(center, Vector3.up, 180f); // Rotate 180 degrees around the Y-axis



            // Move the player to the marker's position
            player.transform.position = marker1.transform.position;

            // Set the rotation to face the right direction
            Vector3 lookDirection = marker1.transform.right;
            player.transform.rotation = Quaternion.LookRotation(lookDirection);

            // Re-enable the CharacterController
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
        else
        {
            isInside = false;
            otherDoorSc.isInside = false;

            //Rotate door and other door
            Vector3 center = this.GetComponent<Renderer>().bounds.center;
            this.transform.RotateAround(center, Vector3.up, 180f); // Rotate 180 degrees around the Y-axis
            //repeat for other door
            center = otherDoor.GetComponent<Renderer>().bounds.center;
            otherDoor.transform.RotateAround(center, Vector3.up, 180f); // Rotate 180 degrees around the Y-axis

            // Move the player to the marker's position
            player.transform.position = marker2.transform.position;

            // Set the rotation to face the right direction
            Vector3 lookDirection = marker2.transform.right;
            player.transform.rotation = Quaternion.LookRotation(lookDirection);

            // Re-enable the CharacterController
            if (controller != null)
            {
                controller.enabled = true;
            }
        }

    }

}
