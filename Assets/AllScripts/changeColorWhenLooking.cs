using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic testing for gvrReticlePointer from google cardboard
// Just changes the color when function assigned to an object
public class changeColorWhenLooking : MonoBehaviour
{
    public void Red() {
        GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Changed color to Blue");
    }

    public void Blue() {
        GetComponent<Renderer>().material.color = Color.blue;
        Renderer curRenderer = GetComponent<Renderer>();
        curRenderer.material.SetColor("_Color", new Color(1f, 1f ,0f));
        Debug.Log("Changed color to Blue");
    }

    public void Black() {
        GetComponent<Renderer>().material.color = Color.black;
        Debug.Log("Changed color to Blue");
    }
}
