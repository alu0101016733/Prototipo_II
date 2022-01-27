using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manage user input commands before sending signals to other
// components
public class userInput : MonoBehaviour
{
    public delegate void userInputDelegation(int action);
    public static event userInputDelegation userInputEvent;

    public GameObject pauseMenuUI;
    Playercontrols controls;
    bool inGame = false;
    bool gameInPause = true;

	// set start variables before starting the actual game
    void Start()
    {
        gameInPause = true;
        inGame = false;
    }

	// when script awakes, add listeners to methods and
	// initialize player controls
    void Awake()
    {
        GameLogicController.menuHandlerDelegation += HandlingFromGameController;
        KeywordVoiceControl.KeywordRecognizerEvent += HandlingFromGameController;
        ButtonOnClickEvents.buttonInputEvent += HandlingFromGameController;
        UserCharacter.userCharacterEvent += HandlingFromGameController;
        controls = new Playercontrols();
        controls.Gameplay.Fire.performed += ctx => Fire();
        controls.Gameplay.Pause.performed += ctx => menu();
        controls.Gameplay.Quit.performed += ctx => quit();
        GetComponent<AudioSource>().Play();
    }

	// uses generals codings to decifer messages that com into
	// the game controller and pauses, resumes or does noting depending
	// on the message code
    void HandlingFromGameController(int state)
    {
        Debug.Log("Handling user control: " + gameInPause);
        if (state == InGameComunicationCodes.showStartMenu)
        {
            inGame = false;
            gameInPause = true;
            PauseTheGame();
        }
        else if (state == InGameComunicationCodes.resumeToCurrentGame)
        {
            inGame = true;
            gameInPause = true;
            menu();
        }
        else if (state == InGameComunicationCodes.togglePause)
        {
            Debug.Log("Toggle game");
            inGame = true;
            menu();
        }
        else if (state == InGameComunicationCodes.quitGame)
        {
            quit();
        }
        else if (state == InGameComunicationCodes.playerDied)
        {
            PauseTheGame();
        }
    }

	// quit the aplication
    void quit()
    {
        if (gameInPause)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }

	// decide if to show menu or not
    void menu()
    {
        Debug.Log("GAME IN PAUSE:" + gameInPause);
        if (gameInPause)
        {
            ResumeToPlay();
        }
        else
        {
            PauseTheGame();
        }
    }

	// resume to current game
    void ResumeToPlay()
    {
        Debug.Log("Resume to play");
        GetComponent<AudioSource>().Play();
        gameInPause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

	// pause the game and show menu
    void PauseTheGame()
    {
        GetComponent<AudioSource>().Pause();
        Debug.Log("Menu pausa");
        gameInPause = true;
        pauseMenuUI.SetActive(true);
        ChangeText();
        Time.timeScale = 0f;
    }

	// delegate the gun to fire
    void Fire()
    {
        //Debug.Log("Fire");
        if (!gameInPause)
        {
            userInputEvent(userInputDefinition.Fire);
        }
    }

	// enable controls when aplication gets enabled
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

	// disable controls when exiting
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

	// change the text of the buttons depending on the current use case of the menu
    void ChangeText()
    {
        //pauseMenuUI.SetActive(true);
        if (inGame)
        {
            GameObject.FindGameObjectWithTag("menu_pause_text").GetComponent<Text>().text = "Resume";
        }
        else
        {
            GameObject.FindGameObjectWithTag("menu_pause_text").GetComponent<Text>().text = "Start";
        }
    }
}

