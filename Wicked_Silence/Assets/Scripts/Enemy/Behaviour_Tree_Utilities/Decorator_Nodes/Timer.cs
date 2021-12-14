using UnityEngine;
using BehaviourTree;
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