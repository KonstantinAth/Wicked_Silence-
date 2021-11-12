using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Player Movement")]
    [SerializeField] [Range(0, 50)] float moveSpeed = 7;
    [SerializeField] [Range(0, 70)] float runSpeed = 15;
    CharacterController playerController;
    [Header("Character Controller Configs")]
    [SerializeField] Vector3 crouchControllerCenter;
    [SerializeField] float crouchControllerHeight;
    //Raycast info...
    [Header("Raycast Info")]
    Ray ray;
    [SerializeField] float maxDistance = 2.0f;
    [SerializeField] LayerMask belowObjectLayerMask;
    //Input floats
    float XInput;
    float ZInput;
    //Boolean to indicate if player is pressing the LShift
    bool IsRunning;
    public bool isCrouching;
    //Player Movement & direction vectors
    Vector3 movement;
    Vector3 direction;
    //Holding the object's original Scale...
    float originalControllerHeight;
    Vector3 originalControllerCenter;
    // Start is called before the first frame update
    void Start() {
        //Initializing character controller
        playerController = GetComponent<CharacterController>();
        //Initializing object's original Controller Configs...
        originalControllerHeight = playerController.height;
        originalControllerCenter = playerController.center;
    }
    // Update is called once per frame
    void Update() {
        Inputs();
        Movement();
        Crouching();
    }
    void Movement() {
        //Change move speed if player input's the LSHift Key...
        if (IsRunning) {
            Debug.Log("RUNNING");
            playerController.Move(direction * runSpeed * Time.deltaTime);
        }
        else {
            playerController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
    //Taking user's inputs...
    void Inputs() {
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        XInput = Input.GetAxisRaw("Horizontal");
        ZInput = Input.GetAxisRaw("Vertical");

        movement = new Vector3(XInput, 0.0f, ZInput);
        direction = transform.TransformDirection(movement).normalized;
    }
    void Crouching() {
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        if(isCrouching || IsBelowObject()) {
            playerController.height = crouchControllerHeight;
            playerController.center = crouchControllerCenter;
        }
        else {
            playerController.height = originalControllerHeight;
            playerController.center = originalControllerCenter;
        }
    }
    public bool IsBelowObject() {
        Ray ray = new Ray(transform.position, Vector3.up);
        bool belowObject = Physics.Raycast(ray, maxDistance, belowObjectLayerMask);
        if(belowObject) {
            return true;
        }
        else {
            return false;
        }
    }
}