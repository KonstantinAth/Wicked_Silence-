using BehaviourTree;
using UnityEngine.AI;
using UnityEngine;
public class GoToAreaPointAction : Node {
    NavMeshAgent agent;
    public GoToAreaPointAction(NavMeshAgent agent) : base() {
        this.agent = agent;
    }
}
