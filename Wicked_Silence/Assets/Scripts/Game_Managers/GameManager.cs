using UnityEngine;
using BehaviourTree;
public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager _instance;
    private void Awake() { _instance = this; }
    #endregion
    public PlayerMovement player;
    public MicrophoneInput micInput;
    // Start is called before the first frame update
    void Start() { Initialization(); }
    // Update is called once per frame
    void Update(){ }
    void Initialization() { HideCursor(); }
    void HideCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}