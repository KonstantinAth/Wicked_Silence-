using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using BehaviourTree;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : NodeTree {
    NavMeshAgent agent;
    [SerializeField] float m_Range;
    [SerializeField] PlayerMovement player;
    [SerializeField] float minimumDistanceFromTheTarget = 4.0f;
    public override void Initialize()  {
        agent = GetComponent<NavMeshAgent>();
        base.Initialize();
    }
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
    //Our parallel takes a sequence of two children, check if we have a destination & Go to destination
    //If both of them are qualified as SUCCEEDED then we are going to move to the specified location...
    protected override Node SetUpTree() {
        Debug.Log("SETTING UP ROOT NODE...");
        _rootNode = new Parallel(); 
        List<Node> destinationSequencer = new List<Node>() {
            new GoToDestinationAction(agent),
            new TrySetChaseDestinationTask(player, transform.position,minimumDistanceFromTheTarget),
            new CheckHasDestinationTask(),
        };
        Node pathSequencer = new Sequencer(destinationSequencer);
        Debug.Log("ATTACHING CHILDREN TO ROOT NODE");
        _rootNode.AttachChild(pathSequencer);
        Debug.Log($"ROOT NODE CHILDREN COUNT => {_rootNode.Children.Count}");
        return _rootNode;
    }
}
