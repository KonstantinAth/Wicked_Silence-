using UnityEngine;
using BehaviourTree;
//Listen Sensor's job is to look & "sense" for the player & give away the player's position...
public class ListenSensor : Node, ISensor {
    public static bool detected = false;
    private bool resetted = false;
    Vector3 currentTransform;
    private float MinDistanceBetween;
    private float minInvestigateDistance;
    private float minGotYouDistance;
    private float minDBInvestigateMeter;
    private float minDBChaseMeter;
    float distanceFromTarget => Vector3.Distance(currentTransform, _instance.player.transform.position);
    bool isInGotYouDistance => distanceFromTarget <= minGotYouDistance;
    bool isInInvestigationDistance => distanceFromTarget <= minInvestigateDistance;
    bool isShouting => _instance.micInput.MicLoudnessDecibels > minDBChaseMeter;
    bool isBeingHeard => _instance.micInput.MicLoudnessDecibels > minDBInvestigateMeter;
    GameManager _instance => GameManager._instance;
    public ListenSensor(Vector3 currentTransform, float MinDistanceBetween ,float minInvestigateDistance, float minGotYouDistance 
        ,float minDBInvestigateMeter, float minDBChaseMeter) : base() {
        this.MinDistanceBetween = MinDistanceBetween;
        this.minInvestigateDistance = minInvestigateDistance;
        this.minGotYouDistance = minGotYouDistance;
        this.minDBInvestigateMeter = minDBInvestigateMeter;
        this.minDBChaseMeter = minDBChaseMeter;
        this.currentTransform = currentTransform;
    }
    public void Detect() {

    }
    public bool Detected() { return detected; }
    public override NodeState Evaluate() {
        if (distanceFromTarget <= MinDistanceBetween)
        {
            _state = NodeState.RUNNING;
            switch (_instance.player.detectionLevel) {
                case PlayerMovement.DetectionLevel.UNDETECTABLE: {
                        //IF HAVEN'T SENSED PLAYER FOR A WHILE...
                        ResetNode();
                        _state = NodeState.FAILED;
                        return _state;
                }
                case PlayerMovement.DetectionLevel.DISCRETE: {
                        if (isInGotYouDistance) {
                            Debug.Log("[ SENSED PLAYER IN GOT YOU DISTANCE ]");
                            HandleDetection(_instance.player.transform.position);
                        }
                        else if (isShouting) {
                            Debug.Log("[ SENSED PLAYER SHOUTING ]");
                            HandleDetection(_instance.player.LastKnownPosition);
                        }
                        else {
                            Debug.Log("[ NOT SENSING PLAYER ]");
                            detected = false;
                            ResetNode();
                            _state = NodeState.FAILED;
                        }
                        return _state;
                }
                case PlayerMovement.DetectionLevel.MEDIUMDISCRETION: {
                        if (isInGotYouDistance) {
                            Debug.Log("[ SENSED PLAYER IN GOT YOU DISTANCE ]");
                            HandleDetection(_instance.player.transform.position);
                        }
                        //IF WE WEREN'T LOUD A FEW MOMENTS AGO, ELSE KEEP CHASING PLAYER...
                        else if (isInInvestigationDistance && isBeingHeard) {
                            Debug.Log("[ SENSED PLAYER IN INVESTIGATION DISTANCE ]");
                            Vector3 position = _instance.micInput.LastKnownPosition;
                            HandleDetection(position);
                        }
                        else if (isShouting) {
                            Debug.Log("[SENSED PLAYER IN CHASE DISTANCE]");
                            //Vector3 position = isInChaseDistance ? _instance.player.LastKnownPosition : _instance.micInput.LastKnownPosition;
                            Vector3 position = _instance.micInput.LastKnownPosition;
                            HandleDetection(position);
                        }
                        else {
                            Debug.Log("[ NOT SENSING PLAYER ]");
                            detected = false;
                            resetted = false;
                            ResetNode();
                            _state = NodeState.FAILED;
                        }
                        return _state;
                }
                case PlayerMovement.DetectionLevel.LOUD: {
                    Debug.Log("[SENSED PLAYER BEING LOUD ]");
                    Vector3 position = _instance.player.transform.position;
                    HandleDetection(position);
                    break;
                }
            }
        }
        else {
            _state = NodeState.FAILED;
        }
        return _state;
    }
    private void HandleDetection(Vector3 targetPosition) {
        detected = true;
        _state = NodeState.SUCCEEDED;
        Parent.SetData("PlayerDestination", targetPosition);
    }
    public override void ResetNode() {
        if (!resetted) {
            ClearData("PlayerDestination");
            resetted = true;
        }
    }
}