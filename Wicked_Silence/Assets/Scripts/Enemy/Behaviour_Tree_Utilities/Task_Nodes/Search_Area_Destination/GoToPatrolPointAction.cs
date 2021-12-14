using BehaviourTree;
using UnityEngine.AI;
using UnityEngine;
public class GoToPatrolPointAction : Node {
    NavMeshAgent agent;
    public GoToPatrolPointAction(NavMeshAgent agent) : base() {
        this.agent = agent;
    }
    //IF THERE IS A PATH TO GO/AREA TO SEARCH RETURN TRUE & THE DESTINATION (Vector3)...
    public (bool, Vector3) FindPath() {
        (bool, Vector3) hasDestination;
        hasDestination = (true, Vector3.zero);
        //TO GO TO PATROL, DOES NOT BELONG HERE...
        //REVISIT COMMENTS ON THE TRY SET DESTINATION TASK NODE...
        //public void Update() {
        //    SimpleNavigation();
        //}
        //public void SimpleNavigation() {
        //    //JUST TO KEEP THE AGENT MOVING... TO BE REMOVED... 
        //    if (agent.pathPending || agent.remainingDistance > 0.1f)
        //        return;
        //    agent.destination = m_Range * Random.insideUnitCircle;
        //}
        return hasDestination;
    }
}
