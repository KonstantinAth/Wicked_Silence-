using UnityEngine;
using System.Collections;
using FMODUnity;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StudioEventEmitter))]
public class PlayerMovement : MonoBehaviour {
    #region Store Player's Detection States/Level
    //Store Player's Detection States/Level
    public enum DetectionLevel {
        unDetectable = 0,
        discrete = 1,
        mediumDiscretion = 2,
        loud = 3
    }
    #endregion
    #region Initialization Variables Organized Under A Header
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
    [Header("Ground Check")]
    [SerializeField] Transform bottomPoint;
    [SerializeField] float checkSphereRadius = 0.5f;
    [SerializeField] LayerMask groundLayer;
    [Header("Physics Configs")]
    [SerializeField] float gravity;
    [SerializeField] float mass;
    Vector3 velocity;
    [Tooltip("Detection Level changes based upon the noise the player makes ")]
    [Header("Detection Level & Data")]
    public DetectionLevel detectionLevel = DetectionLevel.discrete;
    public bool JustGotLoud = false;
    #region Last Known Position 
    public static Vector3 LastKnownPosition;
    public bool HasLastKnowPosition() => LastKnownPosition != null ? true : false;
    #endregion 
    Ray ray;
    //Input floats
    float XInput;
    float ZInput;
    //Boolean to indicate if player is pressing the LShift
    bool IsRunning;
    public bool isWalking;
    [Header("Crouching Controls")]
    public bool isCrouching;
    //Player Movement & direction vectors
    Vector3 movement;
    Vector3 direction;
    //Holding the object's original Scale...
    float originalControllerHeight;
    Vector3 originalControllerCenter;
    PlayerDetectObjects playerHiding;
    #endregion
    #region Unity Default Methods
    // Start is called before the first frame update
    void Start() {
        Init();
    }
    // Update is called once per frame
    void Update() {
        if (!playerHiding.hiding) {
            Inputs();
            Movement();
            Crouching();
        }
    }
    private void FixedUpdate() {
        ApplyGravity();
    }
    #endregion
    #region Initialization
    void Init() {
        //Initializing player components...
        playerController = GetComponent<CharacterController>();
        playerHiding = GetComponent<PlayerDetectObjects>();
        //Initializing object's original Controller Configs...
        originalControllerHeight = playerController.height;
        originalControllerCenter = playerController.center;
    }
    #endregion
    #region Movement & Inputs
    void Movement() {
        //Change move speed if player input's the LSHift Key...
        if (IsRunning) {
            detectionLevel = DetectionLevel.loud;
            playerController.Move(direction * runSpeed * Time.deltaTime);
            if(!JustGotLoud) {
                LastKnownPosition = transform.position;
                JustGotLoud = true;
            }
        }
        else {
            JustGotLoud = false;
            detectionLevel = DetectionLevel.mediumDiscretion;
            playerController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
    //Taking user's inputs...
    void Inputs() {
        //Take user input...
        IsRunning = Input.GetKey(KeyCode.LeftShift) && isWalking && !isCrouching;
        XInput = Input.GetAxisRaw("Horizontal");
        ZInput = Input.GetAxisRaw("Vertical");
        //Configure the movement & direction Vectors...
        movement = new Vector3(XInput, 0.0f, ZInput);
        if(movement.x != 0 || movement.z != 0) { isWalking = true; }
        else { isWalking = false; }
        direction = transform.TransformDirection(movement).normalized;
    }
    #endregion
    #region Apply Gravity
    void ApplyGravity() {
        if (IsGrounded()) { velocity.y = 0; }
        else {
            velocity.y += mass * gravity * Mathf.Pow(Time.deltaTime, 2);
            playerController.Move(velocity);
        }
    }
    bool IsGrounded() {
        bool isGrounded = Physics.CheckSphere(bottomPoint.position, checkSphereRadius, groundLayer);
        if (isGrounded) { return true; }
        else { return false; }
    }
    #endregion
    #region Crouching Controls & Below Object Check
    void Crouching() {
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        if(isCrouching || IsBelowObject()) {
            detectionLevel = DetectionLevel.discrete;
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
    #endregion
    private void OnDrawGizmos() {
        ray = new Ray(transform.position, Vector3.up);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
        Gizmos.DrawWireSphere(bottomPoint.position, checkSphereRadius);
    }
}