using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 500)] float sensitivity;
    [SerializeField] PlayerMovement player;
    Camera mainCamera;
    float RotationY;
    float RotationX;
    // Start is called before the first frame update
    void Start() { mainCamera = Camera.main; }
    // Update is called once per frame
    void Update()
    {
        Inputs();
    }
    void Inputs() {
        RotationY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        RotationY = Mathf.Clamp(RotationY, -45.0f, 45.0f);
        RotationX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(-RotationY, 0.0f, 0.0f);
        player.transform.Rotate(0.0f, RotationX, 0.0f);
    }
}
