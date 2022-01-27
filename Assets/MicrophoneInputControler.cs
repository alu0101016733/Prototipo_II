using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

// Microphone input control, will pick up raw microphone data and convert
// it to a single variable defining the loudness of the user, if it exceeds
// a certain threshold it will delegate to others
public class MicrophoneInputControler : MonoBehaviour
{
    // delegate others when user too loud
    public delegate void microphoneLoudnessDelegation(float peak_, Vector3 position_);
    public static event microphoneLoudnessDelegation microphoneOverThreshold;

    private float MicLoudness;
    private string microphone_;
    bool isInitialized_;

    AudioClip recordedClip_ = null; //new AudioClip();
    int qSamples_ = 1024;
    float referenceValue = 0.1f;

    // initialize microphone
    void Start() {
        InitMic();
    }

    // update microphone data listened to
    void Update() {
        if (isInitialized_) {
            MicLoudness = LevelMax ();
            if (MicLoudness > InGameComunicationCodes.microphoneLimitsUnitlCall) {
                // Debug.Log("Will call microphone subscribers" + MicLoudness);
                microphoneOverThreshold(MicLoudness, GetComponent<Transform>().position);
            }
        }
    }
    
    //mic initialization
    void InitMic(){
        if(microphone_ == null) microphone_ = Microphone.devices[0];
        string[] devices = Microphone.devices;
        if (devices.Length != 0) {
            Debug.Log("Showing available microphone devices:");
            for( var i = 0 ; i < devices.Length ; i++ ) {
                Debug.Log(devices[i]);
            }
            Debug.Log("Using microphone device 0");
            microphone_ = devices[0];
            recordedClip_ = Microphone.Start(microphone_, true, 999, 44100);
            isInitialized_=true;
        }
    }
    
    // stop the microphone
    void StopMicrophone() {
        Microphone.End(microphone_);
    }
    
    //get data from microphone into audioclip
    float  LevelMax() {
        // float sum_ = 0f;
        // float[] waveData = new float[qSamples_];
        // int micPosition = Microphone.GetPosition(null)-(qSamples_+1);
        // if (micPosition < 0) return 0;
        // recordedClip_.GetData(waveData, micPosition);
        // for (int i = 0; i < qSamples_; i++) {
        //     sum_ += waveData[i] * waveData[i];
        // }
        // float rms = Mathf.Sqrt(sum_/qSamples_);
        // int decibel = Mathf.FloorToInt(Mathf.Log10(rms/referenceValue));
        // return decibel;

        float levelMax = 0;
        float[] waveData = new float[qSamples_];
        int micPosition = Microphone.GetPosition(null)-(qSamples_+1); // null means the first microphone
        if (micPosition < 0) return 0;
        recordedClip_.GetData(waveData, micPosition);
        // Getting a peak on the last samples
        for (int i = 0; i < qSamples_; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
    
    //stop mic when loading a new level or quit application
    void OnDisable() {
        StopMicrophone();
    }
    
    void OnDestroy() {
        StopMicrophone();
    }
    
    
    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus) {
        if (focus) {            
            if(!isInitialized_){
                InitMic();
                isInitialized_=true;
            }
        }      
        if (!focus) {
            StopMicrophone();
            isInitialized_=false;            
        }
    }
}
