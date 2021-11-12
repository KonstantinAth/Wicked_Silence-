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
    [SerializeField] float crouchLerp = 5.0f;
    //Raycast info...
    [Header("Raycast Info")]
    [SerializeField] float maxDistance = 2.0f;
    [SerializeField] LayerMask belowObjectLayerMask;
    Ray ray;
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
            playerController.height = Mathf.Lerp(playerController.height, crouchControllerHeight, crouchLerp * Time.deltaTime);
            playerController.center = Vector3.Lerp(playerController.center, crouchControllerCenter, crouchLerp * Time.deltaTime);
        }
        else {
            playerController.height = Mathf.Lerp(playerController.height, originalControllerHeight, crouchLerp * Time.deltaTime);
            playerController.center = Vector3.Lerp(playerController.center, originalControllerCenter, crouchLerp * Time.deltaTime);
        }
    }
    public bool IsBelowObject() {
        ray = new Ray(transform.position, Vector3.up);
        bool belowObject = Physics.Raycast(ray, maxDistance, belowObjectLayerMask);
        if(belowObject) {
            return true;
        }
        else {
            return false;
        }
    }
    private void OnDrawGizmos() {
        ray = new Ray(transform.position, Vector3.up);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
    }
}