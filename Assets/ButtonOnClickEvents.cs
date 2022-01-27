using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnClickEvents : MonoBehaviour
{
    public delegate void buttonInputDelegation(int action);
    public static event buttonInputDelegation buttonInputEvent;
    // Start is called before the first frame update

    public void playOrResumeWasPressed() {
        Debug.Log("Play or Resume Button Pressed");
        buttonInputEvent(InGameComunicationCodes.togglePause);
    }

    public void quitWasPressed() {
        Debug.Log("Quit Button was pressed");
        buttonInputEvent(InGameComunicationCodes.quitGame);
    }
}
