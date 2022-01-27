using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine.Windows.Speech;
// using System.Linq;

public class KeywordVoiceControl : MonoBehaviour
{
    public delegate void userSpokenCommand(int commandCode);
    public static event userSpokenCommand KeywordRecognizerEvent;

    // private KeywordRecognizer keywordRecognizer_ = null;
    // private Dictionary<string, System.Action> keywords_ = new Dictionary<string, System.Action>();
    // private string currentWord_;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     // Define keywords to recognize and assign action to be performed
    //     keywords_.Add("Pause", () => pause());
    //     keywords_.Add("Play", () => play());
    //     keywords_.Add("Resume", () => play());

    //     StartkeywordRecognizer();
    // }

    // void StartkeywordRecognizer() {
    //     keywordRecognizer_ = new KeywordRecognizer(keywords_.Keys.ToArray());
    //     keywordRecognizer_.OnPhraseRecognized += CallWhenRecognized;
    //     if (!keywordRecognizer_.IsRunning) {
    //         keywordRecognizer_.Start();
    //         Debug.Log("Started Keyword Recognition");
    //     }
    // }

    // void StopkeywordRecognizer() {
    //     if (keywordRecognizer_ != null) {
    //         if (keywordRecognizer_.IsRunning) {
    //             Debug.Log("Stop Keyword recognizer");
    //             keywordRecognizer_.Stop();
    //             keywordRecognizer_.Dispose();
    //             PhraseRecognitionSystem.Shutdown(); 
    //         }
    //     }
    //     keywordRecognizer_ = null;
    // }

    // private void CallWhenRecognized(PhraseRecognizedEventArgs args) {
    //     StringBuilder builder = new StringBuilder();
    //     builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
    //     builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
    //     builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
    //     //Debug.Log(builder.ToString());
    //     if (args.confidence != ConfidenceLevel.Rejected) {
    //         PerformActionFor(args.text);
    //     }
    // }

    // private void PerformActionFor(string action) {
    //     //Debug.Log("Will perform action for command: " + action);
    //     if (keywords_.ContainsKey(action)) {
    //         keywords_[action].Invoke();
    //     }
    // }

    // void OnDestroy() {
    //     StopkeywordRecognizer();
    // }

    // void pause() {
    //     Debug.Log("Voice command pause game");
    //     KeywordRecognizerEvent(InGameComunicationCodes.pauseCurrentGame);
    // }

    // void play() {
    //     Debug.Log("Voise command play game");
    //     KeywordRecognizerEvent(InGameComunicationCodes.resumeToCurrentGame);
    // }
}
