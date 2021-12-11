using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Text;
public class VoiceManager : VoiceCommandsManager {
    //The KeywordRecognizer supplies your app with an array of string commands to listen for
    KeywordRecognizer KeywordRecognizer;
    [SerializeField] ConfidenceLevel confidenceLevel = ConfidenceLevel.Rejected;
    [SerializeField] string[] keywordsToFeed;
    Action[] actionsToFeed;
    public string wordSpoken = null;
    float time;
    //Methods provided (& exist in) by the voiceCommandsManager...
    //Initialized in the constructor to be able to use non static members...
    public VoiceManager() {
        actionsToFeed = new Action[] {
            () => Wait(),
            () => Open(),
            () => Seek()
        };
    }
    //A Dictionary in which we can store the keywords the keyword recognizer will listen for
    //with the corresponding actions to take when they are heard...
    Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start() {
        Initialization();
    }
    //Initializing keywords dictionary with the keywords & actions...
    void Initialization() {
        if (keywords != null) {
            for (int i = 0; i < keywordsToFeed.Length; i++) {
                keywords.Add(keywordsToFeed[i], actionsToFeed[i]);
            }
            //Give the array of strings...
            KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), confidenceLevel);
            //Assign the reference to the function to the delegate...
            KeywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
            //Start keyword recognition...
            KeywordRecognizer.Start();
        }
    }
    void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        wordSpoken = args.text;
        Debug.Log($"CONFIDENCE LEVEL : {args.confidence}");
        Action keywordAction;
        //if the keyword key is recognized in our dictionary, call the corresponding value (action)...
        if (keywords.TryGetValue(wordSpoken, out keywordAction)) {
            //IF A PHRASE IS LONGER THAN 2 WORDS THEN WAIT FOR A SPECIFIC TIME (IN SECONDS), BUILD THE STRING & EXECUTE THE
            //ACTION
            keywordAction.Invoke();
        }
    }
    //Use to track time in seconds between words...
    float TrackTime(bool start) {
        float seconds;
        if (start) {
            time += Time.deltaTime;
            seconds = Mathf.FloorToInt(time % 60);
            Debug.Log(seconds);
        }
        else { seconds = 0; }
        return seconds;
    }
    private void OnApplicationQuit() {
        if (KeywordRecognizer != null && KeywordRecognizer.IsRunning) {
            KeywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
            KeywordRecognizer.Stop();
        }
    }
}