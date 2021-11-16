using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    [SerializeField] float maxDetectionDistance;
    [SerializeField] LayerMask hideableObjectLayerMask;
    [SerializeField] UIManager uiManager;
    public bool detectedHideableObject;
    public bool playerWantsToHide; 
    Ray ray;
    RaycastHit hit;
    GameObject hitResult;
    public bool hiding = false;
    // Start is called before the first frame update
    void Start() {
        
    }
    // Update is called once per frame
    void Update() {
        Inputs();
        DetectHideableObject();
    }
    void DetectHideableObject() {
        ray = new Ray(transform.position, transform.forward);
        detectedHideableObject = Physics.Raycast(ray, out hit, maxDetectionDistance, hideableObjectLayerMask);
        if(detectedHideableObject) {
            uiManager.SetCanvasState(true);
            hitResult = hit.rigidbody.gameObject;
            uiManager.SetTextBox(hitResult.GetComponent<Closet>().objectData.name);
            if (playerWantsToHide) {
                if(!hitResult.GetComponent<IHideable>().IsHiding()) {
                    hiding = true;
                    hitResult.GetComponent<IHideable>().Hide();
                }
                else {
                    hiding = false;
                    hitResult.GetComponent<IHideable>().ExitHide();
                }
            }
        }
        else {
            uiManager.SetCanvasState(false);
        }
    }
    public string ReturnObjectType() {
        if(hit.rigidbody != null) {
            hitResult = hit.rigidbody.gameObject;
            return hitResult.GetComponent<Closet>().objectData.name;
        }
        else {
            return null;
        }
    }
    void Inputs() {
        playerWantsToHide = Input.GetKeyDown(KeyCode.E);
    }
    private void OnDrawGizmos() {
        Ray ray = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
