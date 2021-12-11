using UnityEngine;
//FIX
public class VoiceCommandsManager : MonoBehaviour {
    //Fuctions that will be fed onto the Action values of the keywords Dictionary...
    //To be executed when the corresponding key (value) is spoken...
    #region MightNeed
    //!!! WARNING : USE IT TEMPORARILY !!!
    //CHANGE  LOGIC... THIS SCRIPT MUST BE FLEXIBLE & ADAPT TO EACH OBJECT AT THE TIME...
    //IN THIS STATE IT WON'T...
    //#region Initialization
    //#region Open Command Configs
    //[Serializable]
    //private struct DoorData {
    //    public GameObject testDoor => VoiceRecognitionObjectData._instance.doorData.TestDoor;
    //    public float rotationSpeed => VoiceRecognitionObjectData._instance.doorData.rotationSpeed;
    //    public Vector3 rotationAngle => VoiceRecognitionObjectData._instance.doorData.rotationAngle;
    //}
    //#endregion
    //#region Seek Command Configs
    //[Serializable]
    //private struct SeekEnemyData {
    //    public static GameObject EnemyObject => VoiceRecognitionObjectData._instance.enemyData.EnemyObject;
    //    public static RaycastHit EnemyHit => VoiceRecognitionObjectData._instance.enemyData.EnemyHit;
    //    public static bool DetectedEnemy => VoiceRecognitionObjectData._instance.enemyData.DetectedEnemy; 
    //    public static float Distance => VoiceRecognitionObjectData._instance.enemyData.Distance;
    //}
    //#endregion
    //#endregion
    //[SerializeField] DoorData storedDoorData;
    #endregion 
    [Header("Voice Command Configs")]
    [SerializeField] public VoiceManager voiceManager;
    [SerializeField] PlayerDetectObjects detectObjects;
    public bool waiting = false;
    public bool seeking = false;
    public bool opened = false;
    #region Commands
    protected void Wait() {
        Debug.Log("[WAITING]...");
    }
    protected void Seek() {
        Debug.Log("[SEEKING]...");
    }
    protected void Open() {
        if(detectObjects.detectedVoiceInteractableObject) {
            voiceManager.opened = true;
        }
    }
    #endregion
}