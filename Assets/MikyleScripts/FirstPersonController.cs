using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 1500f;

    private float xRotation = 0f;
    private CharacterController controller;
    private Log currentLog; // Reference to the Log the player is sitting on

    float ySpeed;
    float gravity = 0.1f;

    public bool canMove = true;
    public static FirstPersonController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
    }

    void Update()
    {
        // Allow standing up when sitting
        if (Input.GetKeyDown(KeyCode.Space) && !canMove)
        {
            // Call the stand up method from the Log script (if needed)
            StandUp();
        }

        if (canMove)
        {
            // Get mouse input for looking around
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the camera vertically
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent over-rotation
            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Get movement input for WASD keys
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            //// Translate input into world space
            Vector3 move = transform.right * moveX * speed + transform.forward * moveZ * speed;


            //gravity
            ySpeed -= gravity * Time.deltaTime;
            move.y = ySpeed;

            // Move the character
            controller.Move(move * Time.deltaTime);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value; // Set movement flag
    }

    public void SetSittingLog(Log log)
    {
        currentLog = log; // Set reference to the current log the player is sitting on
    }

    private void StandUp()
    {
        canMove = true; // Enable movement

        if (currentLog != null)
        {
            currentLog.PlayerStoodUp(); // Notify the Log that the player stood up
            currentLog = null; // Clear the reference to the current log
        }
    }
}

