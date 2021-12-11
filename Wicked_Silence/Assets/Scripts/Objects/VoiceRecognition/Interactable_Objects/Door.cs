using UnityEngine;
//FIX
[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour {
    [Header("Open Door Needed Data")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float minDistance = 5.0f;
    [SerializeField] Vector3 rotationAngle;
    [SerializeField] VoiceCommandsManager voiceCommands;
    [SerializeField] PlayerMovement player;
    public bool thisDoorOpened = false;
    public bool playerNear;
    Rigidbody rb;
    private void OnEnable() { Initialization(); }
    void Initialization() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void Update() { DetectPlayer();  }
    public void DetectPlayer() {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= minDistance) {
            playerNear = true;
            if (voiceCommands.voiceManager.opened) { DoorOpen(); } }
        else {
            playerNear = false;
            return;
        }
    }
    public void DoorOpen() {
        if (!thisDoorOpened) {
            Debug.Log("[OPENING]");
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(rotationAngle), rotationSpeed * Time.deltaTime);
        }
        if (Mathf.Approximately(transform.rotation.eulerAngles.y, 270)) {
            thisDoorOpened = true;
            voiceCommands.voiceManager.opened = false;
            return;
        }
    }
}
