using UnityEngine.AI;
using UnityEngine;
using BehaviourTree;
public class GoToDestinationAction : Node  {
    NavMeshAgent agent;
    public GoToDestinationAction(NavMeshAgent agent) : base() { this.agent = agent; }
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => [GO TO DESTINATION ACTION]");
        //Get the destination point data...
        object destinationPoint = GetData("Destination");
        Debug.Log($"DESTINATION POINT => {destinationPoint}");
        //If we have stored a destination point..
        if (destinationPoint != null) {
            //Cast the object into a Vector3...
            Vector3 destination = (Vector3)destinationPoint;
            float distance = Vector3.Distance(agent.transform.position, destination);
            //If distance is larger than almost none move to destination...
            if (distance > 0.2f) {
                Debug.Log($"Moving to : => {destination}");
                agent.destination = destination;
            }
            float distanceFromTarget = Vector3.Distance(agent.transform.position, agent.destination);
            if (distanceFromTarget <= agent.stoppingDistance || agent.velocity.sqrMagnitude <= 0)
            {
                Debug.Log("[GO TO DITSANCE ACTION] => SUCCEEDED");
                //I PROBABLY GONNA DELETE THIS... 
                Parent.ClearData("Destination");
                _state = NodeState.SUCCEEDED;
                return _state;
            }
        }
        Debug.Log("[GO TO DITSANCE ACTION] => RUNNING");
        _state = NodeState.RUNNING;
        return _state; 
    }
    /*GO TO DESTINATION*/
}
