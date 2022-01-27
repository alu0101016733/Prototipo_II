using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enables us to get sensor data from smartphone
public class UserSensorData : MonoBehaviour
{
    public delegate void accelerationChange(Vector3 accVec);
    public static event accelerationChange accChange;

    public delegate void compassChange(Quaternion comVec);
    public static event compassChange comChange;

    private Vector3 acc_;
    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start();
        acc_ = Input.acceleration;
        StartCoroutine(enableLocation());
        StartCoroutine(enableCompass());
    }

    // subscribe to own delegates in case no one else subscribes
    void Awake() {
        accChange += emptyForAcceleration;
        comChange += emptyForCompassChange;
    }

    void emptyForAcceleration(Vector3 acc) {}
    void emptyForCompassChange(Quaternion comVec) {}

    // Update sensor data
    void Update()
    {
        if (Input.compass.enabled) {
            Quaternion compass = Quaternion.Euler(0,-Input.compass.magneticHeading,0);
            informCompass(compass);
        }
        Vector3 currentAcc = Input.acceleration;
        if (currentAcc != acc_) {
            acc_ = currentAcc;
            informAcceleration(currentAcc);
        }
    }

    // get compass data
    IEnumerator enableCompass() {
        yield return new WaitForSeconds(2);
        Input.compass.enabled = true;

        int maxWait = 20;
        while (!Input.compass.enabled && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        Debug.Log("Compass State: " + Input.compass.enabled);
        Debug.Log("Compass: " + Input.compass.rawVector + "state: " + Input.compass.enabled);
    }

    // get location from device
    IEnumerator enableLocation() {
        //yield return new WaitForSeconds(5);
        #if UNITY_EDITOR
        //Wait until Unity connects to the Unity Remote, while not connected, yield return null
        while (!UnityEditor.EditorApplication.isRemoteConnected)
        {
            yield return new WaitForSeconds(1);
        }
        #endif
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser) {
            Debug.Log("Location not enabled");
            yield break;
        }
        yield return new WaitForSeconds(2);
        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(2);
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " +
                Input.location.lastData.longitude + " " +
                    Input.location.lastData.altitude + " "+
                        Input.location.lastData.horizontalAccuracy + " " +
                            Input.location.lastData.timestamp);
            Text ctext = GameObject.FindGameObjectWithTag("gps_text_field").GetComponent<Text>();
            ctext.text = "Current Location: " + Input.location.lastData.latitude + " " +
                Input.location.lastData.longitude + " " + Input.location.lastData.altitude;
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    void informAcceleration(Vector3 accVec) {
        //Debug.Log("Acelleration: " + Input.acceleration);
        accChange(accVec);
    }

    void informCompass(Quaternion northPole) {
        //Debug.Log("Compass Data: " + northPole);
        comChange(northPole);
    }
}
