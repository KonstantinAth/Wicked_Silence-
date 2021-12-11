using UnityEngine;
public class GameManager : MonoBehaviour {
    public static GameManager _instance; 
    // Start is called before the first frame update
    void Start() { Initialization(); }
    // Update is called once per frame
    void Update(){ }
    void Initialization() { HideCursor(); }
    void HideCursor() {
        _instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}