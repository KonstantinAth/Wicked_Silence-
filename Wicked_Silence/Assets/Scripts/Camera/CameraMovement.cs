using UnityEngine;
public class CameraMovement : MonoBehaviour {
    [SerializeField] [Range(0, 500)] float sensitivity;
    [SerializeField] PlayerMovement player;
    [SerializeField] PlayerDetectObjects playerHiding;
    [SerializeField] float originalPosition = 1.0f;
    [SerializeField] float crouchingYPosition;
    [SerializeField] float crouchMultiplier;
    [SerializeField] float hidingRotationClamp = 70;
    Camera mainCamera;
    float RotationY;
    float RotationX;
    // Start is called before the first frame update
    Vector3 placeholderPosition;
    Vector3 crouchPosition;
    void Start() {
        mainCamera = Camera.main;
        placeholderPosition = new Vector3(0.0f, originalPosition, 0.0f);
        transform.localPosition = placeholderPosition;
    }
    // Update is called once per frame
    void Update() {
        Inputs();
        if (!playerHiding.hiding) { Crouching(); }
    }
    void Inputs() {
        RotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        RotationX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        if (playerHiding.hiding) {
            RotationY = Mathf.Clamp(RotationY, -30, 30);
            RotationX = 0;
        }
        else { RotationY = Mathf.Clamp(RotationY, -hidingRotationClamp, hidingRotationClamp); }
        transform.localRotation = Quaternion.Euler(RotationY, 0.0f, 0.0f);
        player.transform.Rotate(0.0f, RotationX, 0.0f);
    }
    void Crouching() {
        crouchPosition = new Vector3(0.0f, crouchingYPosition, 0.0f); ;
        Vector3 positionAtTheTime = new Vector3(0.0f, transform.localPosition.y, 0.0f);
        if (player.isCrouching || player.IsBelowObject()) { mainCamera.transform.localPosition = Vector3.Lerp(positionAtTheTime, crouchPosition, crouchMultiplier * Time.deltaTime); }
        else {
            if (mainCamera.transform.localPosition != placeholderPosition) {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, placeholderPosition, crouchMultiplier * Time.deltaTime);
            }
        }
    }
}