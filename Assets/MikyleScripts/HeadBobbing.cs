using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public Camera playerCamera;  // Reference to the player camera
    public float bobbingSpeed = 0.02f;  // Speed of the bobbing
    public float bobbingAmount = 0.05f; // How much the head bobs

    private float defaultPosY = 0;
    private float timer = 0;

    void Start()
    {
        defaultPosY = playerCamera.transform.localPosition.y; // Set default position Y
    }

    void Update()
    {
        //Debug.Log(Time.deltaTime);
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check for movement
        if (horizontal != 0 || vertical != 0)
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }
        else
        {
            timer = 0; // Reset timer if not moving
        }

        // Calculate the new position for bobbing
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultPosY + translateChange,
                playerCamera.transform.localPosition.z
            );

            // Debugging output
            //Debug.Log("Waveslice: " + waveslice);
            //Debug.Log("Translate Change: " + translateChange);
        }
        else
        {
            // Reset to default position when player stops
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultPosY,
                playerCamera.transform.localPosition.z
            );
        }
    }
}
