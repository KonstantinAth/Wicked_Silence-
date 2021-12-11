using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
public class MicrophoneInput : MonoBehaviour {
    #region Singleton
    public static MicrophoneInput _instance;
    public static MicrophoneInput GetInstance() { return _instance; }
    #endregion
    public static Vector3 LastKnownPosition;
    public static bool HasLastKnowPosition() => LastKnownPosition != null ? true : false;
    public static float MicLoudness;
    public static float MicLoudnessDecibels;
    public static bool _IsInitialized;
    public static bool JustGotLoud = false;
    public bool GotLoud;
    private string _micDevice;
    private int frequency = 44100;
    [SerializeField] AudioSource audioSource;
    [Header("Player Detection")]
    [SerializeField] private float detectableDBValue;
    [SerializeField] PlayerMovement player;
    AudioClip _clipRecord;
    AudioClip _recordedClip;
    int initialSamples = 128;
    void InitializeMic() {
        _micDevice = Microphone.devices[0];
        audioSource.clip = Microphone.Start(_micDevice, true, 2, frequency);
        _clipRecord = audioSource.clip; 
        _IsInitialized = true;
    }
    void StopMic() {
        Microphone.End(_micDevice);
        _IsInitialized = false;
    }
    //get data from microphone into audioclip
    float MicrophoneLevelMax() {
        float levelMax = 0;
        float[] waveData = new float[initialSamples];
        int micPosition = Microphone.GetPosition(null) - (initialSamples + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < initialSamples; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
    //Measure Microphone DB level...
    float MicrophoneLevelMaxDecibels() {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));
        return db;
    }

    public float FloatLinearOfClip(AudioClip clip) {
        StopMic();
        _recordedClip = clip;
        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];
        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
    public float DecibelsOfClip(AudioClip clip) {
        StopMic();
        _recordedClip = clip;
        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];
        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));
        return db;
    }
    //Store player's position if they got loud
    public void DetectPlayerByDB() {
        if(MicLoudnessDecibels > detectableDBValue) {
            if (!JustGotLoud) {
                LastKnownPosition = player.transform.position;
                JustGotLoud = true;
            }
        }
        else {
            JustGotLoud = false;
        }
    }
    #region Unity Methods & Calls
    void Awake() { _instance = this; }
    // start mic when scene starts
    void OnEnable() {
        InitializeMic();
        _IsInitialized = true;
    }

    // Update is called once per frame
    void Update() {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessDecibels = MicrophoneLevelMaxDecibels();
        GotLoud = JustGotLoud;
        DetectPlayerByDB();
        //Debug.Log($"MicLoudnessDecibels {MicLoudnessDecibels}");
        //Debug.Log($"MicLoudness {MicLoudness}");
    }
    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMic();
    }
    void OnDestroy()
    {
        StopMic();
    }
    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus) {
        if (focus) {
            Debug.Log("Focus");
            if (!_IsInitialized) {
                Debug.Log("Init Mic");
                InitializeMic();
            }
        }
        if (!focus) {
            Debug.Log("Pause");
            StopMic();
            Debug.Log("Stop Mic");
        }
    }
    #endregion
}