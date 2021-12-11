using UnityEngine;
//Change it so the player won't have to look at the object to stop hiding...
public class PlayerDetectObjects : MonoBehaviour {
    [Header("Object Detection Configs")]
    [SerializeField] float maxDetectionDistance;
    [SerializeField] float maxVoiceInteractableDistance;
    [SerializeField] LayerMask hideableObjectLayerMask;
    [SerializeField] LayerMask voiceInteractableLayerMask;
    [SerializeField] Transform flashlightPoint;
    [SerializeField] UIManager uiManager;
    public bool detectedHideableObject;
    public bool detectedVoiceInteractableObject;
    //Check for input...
    public bool playerWantsToHide => Input.GetKeyDown(KeyCode.E);
    Ray ray => new Ray(flashlightPoint.transform.position, flashlightPoint.transform.forward);
    RaycastHit hideableObjectHit;
    RaycastHit voiceRaycastHit;
    GameObject hideableHitResult;
    GameObject voiceInteractableHitResult;
    public bool hiding = false;
    string typeToReturn;
    Transform transformToReturn;
    // Update is called once per frame
    void Update() { DetectObjects(); }
    void DetectObjects() {
        DetectHideableObjectAction();
        DetectVoiceInteractableObject();
    }
    #region Hideable Object Detection
    //If players Detect a HideableObject we store the data we need...
    void DetectHideableObjectAction() {
        detectedHideableObject = Physics.Raycast(ray, out hideableObjectHit, maxDetectionDistance, hideableObjectLayerMask);
        if (!detectedVoiceInteractableObject) {
            if (detectedHideableObject) {
                hideableHitResult = hideableObjectHit.rigidbody.gameObject;
                if (!hiding) { uiManager.SetCanvasState(true); }
                else { uiManager.SetCanvasState(false); }
                uiManager.SetTextBox(hideableHitResult.GetComponent<HideableObject>().hideableObjectData.name);
                typeToReturn = hideableHitResult.GetComponent<HideableObject>().hideableObjectData.type;
                transformToReturn = hideableHitResult.GetComponent<HideableObject>().hideableObjectData.position;
            }
            else {
                uiManager.SetCanvasState(false);
                return;
            }
        }
    }
    //Return Object type (closed, open)...
    public string GetObjectType() {
        if (hideableObjectHit.rigidbody != null) {
            return typeToReturn;
        }
        else {
            return null;
        }
    }
    //Return object's transform...
    public Transform GetObjectPosition() {
        if (hideableObjectHit.rigidbody != null) {
            return transformToReturn;
        }
        else {
            return null;
        }
    }
    //Return hitResult GameObject...
    public GameObject GetHitResult() {
        if (hideableObjectHit.rigidbody != null) {
            return hideableHitResult;
        }
        else {
            return null;
        }
    }
    #endregion
    #region Voice Interactable Objects
    public void DetectVoiceInteractableObject() {
        detectedVoiceInteractableObject = Physics.Raycast(ray, out voiceRaycastHit, maxVoiceInteractableDistance, voiceInteractableLayerMask);
        if (!detectedHideableObject) {
            if (detectedVoiceInteractableObject) {
                uiManager.SetCanvasState(true);
                voiceInteractableHitResult = voiceRaycastHit.rigidbody.gameObject;
                uiManager.SetTextBox(voiceInteractableHitResult.name);
            }
            else {
                uiManager.SetCanvasState(false);
                return;
            }
        }
    }
    public GameObject GetVoiceInteractableHitResult() {
        if(voiceRaycastHit.rigidbody != null) {
            return voiceInteractableHitResult;
        }
        else {
            return null;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(flashlightPoint.transform.position, flashlightPoint.transform.forward);
    }
    #endregion
}