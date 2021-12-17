using BehaviourTree;
using UnityEngine;
public class CheckHasPointInAreaTask : Node {
  public CheckHasPointInAreaTask() : base() { }
    #region Check If We Have A Point To Go To In The Search Area
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => [ CHECK HAS POINT IN AREA NODE ]");
        if(GetData("SearchDestination") != null) {
            _state = NodeState.SUCCEEDED;
            return _state;
        }
        _state = NodeState.FAILED;
        return _state;
    }
    #endregion
}
