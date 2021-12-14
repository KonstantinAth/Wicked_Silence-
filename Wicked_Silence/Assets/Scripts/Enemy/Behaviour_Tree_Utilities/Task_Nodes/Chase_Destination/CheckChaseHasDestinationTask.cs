using BehaviourTree;
using UnityEngine;
//Check if we have a destination to go to...
public class CheckChaseHasDestinationTask : Node {
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => [CHECK HAS DESTINATION TASK]");
        object destinationPoint = GetData("PlayerDestination");
        if(destinationPoint == null) {
            _state = NodeState.FAILED;
            return _state;
        }
        _state = NodeState.SUCCEEDED;
        return _state;
    }
}