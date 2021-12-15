using UnityEngine;
using BehaviourTree;
//THIS IS NOT THE APPROACH I AM LOOKING FOR,
//THE CONSTRUCTOR WILL RECEIVE A SPECIFIED TIME AS AN ARGUMENT
//AND WHEN WE ARE JUST WENT OFF DETECTED WE ARE GOING TO DECREASE THAT TIME
//WHEN WE ARE DETECTED AGAIN THAT TIME OF GOING TO GO BACK TO IT'S ORIGINAL VALUE...
public class Timer : Node {
    private float timeToHold;
    public Timer(float timeToHold) : base() {
        this.timeToHold = timeToHold;
    }
    public override NodeState Evaluate() {
        float nextCircle = 0;
        if(nextCircle > Time.time) {
            nextCircle = nextCircle + timeToHold;
            _state = NodeState.SUCCEEDED;
        }
        else { _state = NodeState.RUNNING; }
        return _state;
    }
}