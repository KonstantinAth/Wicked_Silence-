using UnityEngine;
public class HidingCamera : MonoBehaviour {
    [SerializeField] PlayerDetectObjects playerHiding;
    [SerializeField] float transitionTime = 5.0f;
    Transform hideableObjectToGoTo => playerHiding.GetObjectPosition();
    private void Update() { HidingCameraAction(); }
    void HidingCameraAction(){
        if (playerHiding.hiding) { TransitionCamera(); }
        else { return; }
    }
    void TransitionCamera() {
        transform.position = Vector3.Lerp(transform.position, hideableObjectToGoTo.position, transitionTime * Time.deltaTime);
    }
}