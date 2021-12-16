using UnityEngine;
using BehaviourTree;
//THIS IS NOT THE APPROACH I AM LOOKING FOR,
//THE CONSTRUCTOR WILL RECEIVE A SPECIFIED TIME AS AN ARGUMENT
//AND WHEN WE ARE JUST WENT OFF DETECTED WE ARE GOING TO DECREASE THAT TIME
//WHEN WE ARE DETECTED AGAIN THAT TIME OF GOING TO GO BACK TO IT'S ORIGINAL VALUE...
public class Timer : Node {
    public static float ORIGINAL_TIME;
    private float timeToHold;
    bool detected => ListenSensor.detected;
    public Timer(float timeToHold) : base() {
        this.timeToHold = timeToHold;
    }
    //If detected then hold & decrease a time to become undetected again...
    public override NodeState Evaluate() {
        if (detected) {
            if (timeToHold > 0) {
                Debug.Log($"TIME TO HOLD");
                timeToHold -= Time.deltaTime;
                _state = NodeState.RUNNING;
            }
            if(timeToHold <= 0) {
                Debug.Log("UN DETECTED");
                ListenSensor.detected = false;
                _state = NodeState.SUCCEEDED;
               
            }
            return _state;
        } 
        else {
            _state = NodeState.FAILED;
            return _state;
        }
    }
    public override void ResetNode() { timeToHold = ORIGINAL_TIME; }
}
//float nextCircle = 0;
//if(nextCircle > Time.time) {
//    nextCircle = nextCircle + timeToHold;
//    _state = NodeState.SUCCEEDED;
//}
//else { _state = NodeState.RUNNING; }
//return _state;