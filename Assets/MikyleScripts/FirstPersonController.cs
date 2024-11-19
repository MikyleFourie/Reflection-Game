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

    public Camera playerCamera;  // Reference to the player camera
    public float bobbingSpeed = 0.02f;  // Speed of the bobbing
    public float bobbingAmount = 0.05f; // How much the head bobs
    float initialbobbingSpeed;  // Speed of the bobbing
    float initialbobbingAmount; // How much the head bobs

    private float defaultPosY = 0;
    private float timer = 0;

    public float fovangle = 25f;
    public List<GameObject> interactableObjects;
    IInteractable currentInteractable = null; // Cache the current interactable object

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
        defaultPosY = playerCamera.transform.localPosition.y; // Set default position Y
        initialbobbingAmount = bobbingAmount;
        initialbobbingSpeed = bobbingSpeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
        Cursor.visible = false;
    }

    void Update()
    {
        if (interactableObjects.Count > 0)
        {
            CheckFOVInteractivity();
        }

        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.Interact(); // Call the interact method on the object
            currentInteractable = null;
        }



        // Allow standing up when sitting
        if (Input.GetKeyDown(KeyCode.Space) && !canMove)
        {
            Debug.Log("Space was pressed");
            // Call the stand up method from the Log script (if needed)
            StandUp();
        }

        if (canMove)
        {
            bobbingSpeed = initialbobbingSpeed;
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
            HeadBob();
        }
        else
        {
            bobbingSpeed = 0f;
        }
    }

    void CheckFOVInteractivity()
    {
        currentInteractable = null;
        //Debug.Log("Checking list");
        foreach (var interactableObj in interactableObjects)
        {
            Outline outlineScript = interactableObj.GetComponent<Outline>();

            Vector3 directionToObject = (interactableObj.transform.position - playerCamera.transform.position).normalized;
            Vector3 forwardDirection = playerCamera.transform.forward;

            float angleToTarget = Vector3.Angle(forwardDirection, directionToObject);
            //Debug.Log("Angle to target: " + angleToTarget);
            if (angleToTarget <= fovangle / 2)
            {
                //Object is within vision cone
                //Debug.Log(interactableObj.name + " is in range");
                currentInteractable = interactableObj.transform.GetComponent<IInteractable>();
                outlineScript.OutlineColor = Color.cyan;
                outlineScript.OutlineWidth = 10;
            }
            else
            {
                //Debug.Log(interactableObj.name + " is NOT in range");
                outlineScript.OutlineColor = Color.white;
                outlineScript.OutlineWidth = 6;
            }

            // At the end of the loop, if currentInteractableObject is null, there is no interactable in view.
            if (currentInteractable == null)
            {
                //Debug.Log("No interactable object in view");
            }
            else
            {
                Debug.Log("Current interactable object: " + currentInteractable);
            }
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
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Debug.Log("Stand Up Function in FPCon activtated");

        canMove = true; // Enable movement

        if (currentLog != null)
        {
            currentLog.PlayerStoodUp(); // Notify the Log that the player stood up
            currentLog = null; // Clear the reference to the current log
        }
    }

    private void HeadBob()
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Souvenir") || other.CompareTag("Interactable"))
        {
            Debug.Log("Entered Trigger of : " + other.name);
            interactableObjects.Add(other.gameObject);
            Outline outlineScript = other.GetComponent<Outline>();
            outlineScript.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Souvenir") || other.CompareTag("Interactable"))
        {
            interactableObjects.Remove(other.gameObject);
            Outline outlineScript = other.GetComponent<Outline>();
            outlineScript.enabled = false;
        }
    }
}

