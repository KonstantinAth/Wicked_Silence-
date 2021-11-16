using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    [SerializeField] float maxDetectionDistance;
    [SerializeField] LayerMask hideableObjectLayerMask;
    public bool detectedHideableObject;
    public bool playerWantsToHide; 
    Ray ray;
    RaycastHit hit;
    GameObject hitResult;
    // Start is called before the first frame update
    void Start()
    {
        
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
            hitResult = hit.rigidbody.gameObject;
            if (playerWantsToHide) {
                if(!hitResult.GetComponent<IHideable>().IsHiding()) {
                    hitResult.GetComponent<IHideable>().Hide();
                }
                else {
                    hitResult.GetComponent<IHideable>().ExitHide();
                }
            }
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
