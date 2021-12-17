using BehaviourTree;
using UnityEngine.AI;
using UnityEngine;
public class GoToAreaPointAction : Node {
    NavMeshAgent agent;
    public GoToAreaPointAction(NavMeshAgent agent) : base() {
        this.agent = agent;
    }
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING : => GO TO AREA POINT ACTION...");
        object positionData = GetData("SearchDestination");
        if(positionData != null) {
            Vector3 areaPoint = (Vector3)positionData;
            float distance = Vector3.Distance(agent.transform.position, areaPoint);
            if(distance > agent.stoppingDistance) {
                Debug.Log($"MOVING TO : => {areaPoint}");
                agent.destination = areaPoint;
            }
            float distanceFromTarget = Vector3.Distance(agent.transform.position, agent.destination);
            if(distance <= agent.stoppingDistance || agent.velocity.sqrMagnitude <= 0) {
                Debug.Log("NODE : [ GO TO AREA POINT ACTION ] STATE => SUCCEEDED...");
                Parent.ClearData("SearchDestination");
                Debug.Log("CLEARED NODE DATA => Search Destination...");
                _state = NodeState.SUCCEEDED;
                return _state;
            }
        }
        Debug.Log("[ GO TO AREA POINT ACTION ] STATE => RUNNING...");
        _state = NodeState.RUNNING;
        return _state;
        throw new System.NotImplementedException();
    }
}
