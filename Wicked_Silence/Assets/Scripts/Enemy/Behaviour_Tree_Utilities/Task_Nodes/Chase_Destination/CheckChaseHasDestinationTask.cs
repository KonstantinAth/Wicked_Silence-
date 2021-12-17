using BehaviourTree;
using UnityEngine;
//Check if we have a destination to go to...
public class CheckChaseHasDestinationTask : Node {
    public CheckChaseHasDestinationTask() : base() { }
    #region Check if we have a destination to chase / follow
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => [CHECK HAS DESTINATION TASK]");
        object destinationPoint = GetData("PlayerDestination");
        if(destinationPoint == null) {
            Debug.Log("[CHECK HAS DESTINATION TASK] => FAILED");
            _state = NodeState.FAILED;
            return _state;
        }
        Debug.Log("[CHECK HAS DESTINATION TASK] => SUCCEEDED");
        _state = NodeState.SUCCEEDED;
        return _state;
    }
    #endregion
}