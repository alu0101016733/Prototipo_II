using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to hold click event functions for game and
// subscriber methods so that other ones can get notified
public class ButtonOnClickEvents : MonoBehaviour
{
    // delegate to inform over click events
    public delegate void buttonInputDelegation(int action);
    public static event buttonInputDelegation buttonInputEvent;

    // Function for button that will pause or play the game
    public void playOrResumeWasPressed() {
        Debug.Log("Play or Resume Button Pressed");
        buttonInputEvent(InGameComunicationCodes.togglePause);
    }

    // function to assign to button when we want to quit game
    public void quitWasPressed() {
        Debug.Log("Quit Button was pressed");
        buttonInputEvent(InGameComunicationCodes.quitGame);
    }
}

