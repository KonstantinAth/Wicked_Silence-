using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] [Range(0, 50)] float moveSpeed = 7;
    [SerializeField] [Range(0, 70)] float runSpeed = 15;
    CharacterController playerController;
    float XInput;
    float ZInput;
    bool IsRunning;
    Vector3 movement;
    Vector3 direction;
    // Start is called before the first frame update
    void Start() {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Inputs();
        Movement();
    }
    void Movement() {
        if (IsRunning) {
            Debug.Log("RUNNING");
            playerController.Move(direction * runSpeed * Time.deltaTime);
        }
        else {
            playerController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
    void Inputs() {
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        XInput = Input.GetAxisRaw("Horizontal");
        ZInput = Input.GetAxisRaw("Vertical");

        movement = new Vector3(XInput, 0.0f, ZInput);
        direction = transform.TransformDirection(movement).normalized;
    }
}
