using UnityEngine;
using BehaviourTree;
//THE CONSTRUCTOR WILL RECEIVE A SPECIFIED TIME AS AN ARGUMENT
//AND WHEN WE LOSE THE PLAYER ( PLAYER DETECTED ) WE ARE GOING TO DECREASE THAT TIME TO GIVE TIME TO THE ENEMY TO SPOT THE PLAYER AGAIN (IF IT CAN)
//WHEN WE ARE DETECTED AGAIN THAT SPECIFIED TIME IS GOING TO GO BACK TO IT'S ORIGINAL VALUE...
public class Timer : Node {
    private static float timeToWait;
    public static float ORIGINAL_TIME_INsecs = 10.0f;
    //Keep track of when the player got away...
    public static bool playerGotAway = false;
    public Timer(float timeToHold) : base() {
        Timer.timeToWait = timeToHold;
    }
    //If detected then keep on searching / moving to the last knownposition & decrease a certain amount of  time
    //to become undetected again...
    #region Keep a timer everytime we are detected, so that we won't stop chasing the player immediately
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => TIMER NODE");
        Debug.Log($"PLAYER GOT AWAY => {playerGotAway}");
        Debug.Log($"TIME TO HOLD => {ORIGINAL_TIME_INsecs}");
        Debug.Log($"TIME TO HOLD [DETECTED] => {ListenSensor.detected}");
        //If player hasn't gotten away && has been detected
        if (!ListenSensor.detected) {
            if (timeToWait > 0) {
                if (!ListenSensor.detected) {
                    timeToWait -= Time.deltaTime;
                    Debug.Log($"TIME TO HOLD : {timeToWait}");
                    _state = NodeState.RUNNING;
                    if(timeToWait <= 0) {
                        timeToWait = 0;
                    }
                    if (timeToWait <= 0 && !ListenSensor.detected)
                    {
                        playerGotAway = true;
                        Debug.Log("UN DETECTED");
                        _state = NodeState.FAILED;
                    }
                }
            }
        }
        else {
            ResetNode();
            playerGotAway = false;
            Debug.Log("FOUND PLAYER AGAIN, [ TIMER STATE ] => SUCCEEDED");
            _state = NodeState.SUCCEEDED;
        }
        return _state;
    }
    #endregion
    public override void ResetNode() {
        timeToWait = ORIGINAL_TIME_INsecs;
    }
}
//  else
//{
//    ResetNode();
//    _state = NodeState.RUNNING;
//    return _state;
//}
//float nextCircle = 0;
//if(nextCircle > Time.time) {
//    nextCircle = nextCircle + timeToHold;
//    _state = NodeState.SUCCEEDED;
//}
//else { _state = NodeState.RUNNING; }
//return _state;