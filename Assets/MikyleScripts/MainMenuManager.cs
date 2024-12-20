using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    CampfireSceneManager sceneManager;
    public GameObject masterPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject warningPanel;
    public GameObject backButton;
    public GameObject exitButton;
    public GameObject continueButton;
    public TextMeshProUGUI Title;
    public string defaultText = "Roadtrip";
    public Slider mouseSenseSlider;
    public Slider walkSpeedSlider;
    public Slider dialogueSpeedSlider;
    GameObject firstActiveChild;

    GameObject player;
    FirstPersonController firstPersonController;
    DialogueManager2 dialogueManager;
    //bool isStart = true;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            player = GameObject.FindWithTag("Player");
            firstPersonController = player.GetComponent<FirstPersonController>();
            dialogueManager = FindObjectOfType<DialogueManager2>();

            backButton.SetActive(false);
            exitButton.SetActive(true);
            continueButton.SetActive(false);
            menuPanel.SetActive(true);
            settingsPanel.SetActive(false);
            controlsPanel.SetActive(false);
            warningPanel.SetActive(false);

            //firstPersonController = player.GetComponent<FirstPersonController>();
            sceneManager = FindObjectOfType<CampfireSceneManager>();
            firstActiveChild = GetFirstActiveChild(menuPanel);
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            firstPersonController = player.GetComponent<FirstPersonController>();
            dialogueManager = FindObjectOfType<DialogueManager2>();

            backButton.SetActive(false);
            exitButton.SetActive(true);
            continueButton.SetActive(false);
            menuPanel.SetActive(true);
            settingsPanel.SetActive(false);
            controlsPanel.SetActive(false);
            warningPanel.SetActive(false);
            masterPanel.SetActive(false);

            //firstPersonController = player.GetComponent<FirstPersonController>();
            sceneManager = FindObjectOfType<CampfireSceneManager>();
            firstActiveChild = GetFirstActiveChild(menuPanel);
        }



    }

    public void showWarning()
    {
        EventSystem.current.SetSelectedGameObject(null);
        backButton.SetActive(false);
        exitButton.SetActive(false);
        continueButton.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        warningPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void closeAll()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
        Cursor.visible = false;
        masterPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void saveSettings()
    {
        firstPersonController.speed = walkSpeedSlider.value;
        firstPersonController.mouseSensitivity = mouseSenseSlider.value * 100;
        dialogueManager.dialogueSpeed = dialogueSpeedSlider.value / 1000;
        GoToMenu();
    }

    public void StartScene()
    {
        //Debug.Log("StartScene ran");
        defaultText = "";
        continueButton.SetActive(true);

        //Debug.Log("StartScene ran");
        masterPanel.SetActive(false);
        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
        Cursor.visible = false;
        firstActiveChild.SetActive(false);
        sceneManager.BeginScene();
    }

    public void GoToMenu()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        Title.text = defaultText;
        if (Title.text == "")
        {
            //Debug.Log("Title was empty");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        backButton.SetActive(false);
        exitButton.SetActive(true);
        if (Title.text == "")
            continueButton.SetActive(true);

        //Debug.Log("GoToMenu ran");
        EventSystem.current.SetSelectedGameObject(null);
        masterPanel.SetActive(true);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        warningPanel.SetActive(false);

        firstActiveChild = GetFirstActiveChild(menuPanel);
        if (firstActiveChild != null)
        {
            EventSystem.current.SetSelectedGameObject(firstActiveChild);
        }
    }

    public void GoToSettings()
    {
        //Debug.Log("GoToSettings ran");

        EventSystem.current.SetSelectedGameObject(null);

        backButton.SetActive(true);
        exitButton.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        warningPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void GoToControls()
    {
        //Debug.Log("GoToControls ran");

        EventSystem.current.SetSelectedGameObject(null);

        backButton.SetActive(true);
        exitButton.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        warningPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("Input Detected");
            if (!masterPanel.activeSelf && player.GetComponent<FirstPersonController>().enabled)
            {
                GoToMenu();
            }
            else if (masterPanel.activeSelf)
            {
                showWarning();
            }

        }
    }

    GameObject GetFirstActiveChild(GameObject parent)
    {
        // Iterate through all children of the parent GameObject
        foreach (Transform child in parent.transform)
        {
            // Check if the child is active
            if (child.gameObject.activeSelf)
            {
                return child.gameObject; // Return the first active child
            }
        }
        // If no active child is found, return null
        return null;
    }
}
