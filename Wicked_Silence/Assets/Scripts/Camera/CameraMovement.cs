using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 500)] float sensitivity;
    [SerializeField] PlayerMovement player;
    [SerializeField] float originalPosition = 1.0f;
    [SerializeField] float crouchingYPosition;
    [SerializeField] float crouchMultiplier;
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
    void Update()
    {
        Inputs();
        Crouching();
    }
    void Inputs() {
        RotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        RotationX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        RotationY = Mathf.Clamp(RotationY, -70, 70);
        transform.localRotation = Quaternion.Euler(RotationY, 0.0f, 0.0f);
        //transform.Rotate(-RotationY, 0.0f, 0.0f, Space.Self);
        player.transform.Rotate(0.0f, RotationX, 0.0f);
    }
    void Crouching() {
        crouchPosition = new Vector3(0.0f, crouchingYPosition, 0.0f); ;
        Vector3 positionAtTheTime = new Vector3(0.0f, transform.localPosition.y, 0.0f);
        if (player.isCrouching || player.IsBelowObject()) {
            Debug.Log("[CROUCHING]");
            mainCamera.transform.localPosition = Vector3.Lerp(positionAtTheTime, crouchPosition, crouchMultiplier * Time.deltaTime);
        }
        else {
            if (mainCamera.transform.localPosition != placeholderPosition) {
                Debug.Log("[STANDING]");
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, placeholderPosition, crouchMultiplier * Time.deltaTime);
            }
        }
    }
}