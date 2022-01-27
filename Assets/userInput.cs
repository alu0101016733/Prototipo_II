using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class userInput : MonoBehaviour
{
  public delegate void userInputDelegation(int action);
  public static event userInputDelegation userInputEvent;

  public GameObject pauseMenuUI;
  Playercontrols controls;
  bool inGame = false;
  bool gameInPause = true;
    void Start() {
      gameInPause = true;
      inGame = false;
    }
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
    void HandlingFromGameController(int state) {
      Debug.Log("Handling user control: "+gameInPause);
      if (state == InGameComunicationCodes.showStartMenu) {
        inGame = false;
        gameInPause = true;
        PauseTheGame();
      } else if (state == InGameComunicationCodes.resumeToCurrentGame) {
        inGame = true;
        gameInPause = true;
        menu();
      } else if (state == InGameComunicationCodes.togglePause) {
        Debug.Log("Toggle game");
        inGame = true;
        menu();
      } else if (state == InGameComunicationCodes.quitGame) {
        quit();
      } else if (state == InGameComunicationCodes.playerDied) {
        PauseTheGame();
      }
    }

  void quit() {
    if (gameInPause) {
      Debug.Log("quit");
      Application.Quit();
    }
  }

  void menu() {
    Debug.Log("GAME IN PAUSE:"+gameInPause);
    if (gameInPause) {
      ResumeToPlay();
    } else {
      PauseTheGame();
    }
  }
  void ResumeToPlay() {
    Debug.Log("Resume to play");
    GetComponent<AudioSource>().Play();
    gameInPause = false;
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
  }
  void PauseTheGame() {
    GetComponent<AudioSource>().Pause();
    Debug.Log("Menu pausa");
    gameInPause = true;
    pauseMenuUI.SetActive(true);
    ChangeText();
    Time.timeScale = 0f;
  }
  void Fire() {
    //Debug.Log("Fire");
    if (!gameInPause) {
      userInputEvent(userInputDefinition.Fire);
    }  
  }
  void OnEnable() {
    controls.Gameplay.Enable();
  }
  void OnDisable() {
    controls.Gameplay.Disable();
  }

  void ChangeText() {
    //pauseMenuUI.SetActive(true);
    if (inGame) {
      GameObject.FindGameObjectWithTag("menu_pause_text").GetComponent<Text>().text = "Resume";
    } else {
      GameObject.FindGameObjectWithTag("menu_pause_text").GetComponent<Text>().text = "Start";
    }
  }
}

