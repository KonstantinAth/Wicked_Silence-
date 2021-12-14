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
    //Our parallel takes a sequence of two children, check if we have a destination & Go to destination
    //If both of them are qualified as SUCCEEDED then we are going to move to the specified location...
    protected override Node SetUpTree() {
        Debug.Log("SETTING UP ROOT NODE...");
        _rootNode = new Selector();
        Node pathSequencer;
        List<Node> destinationSequencer = new List<Node>() {
            new GoToDestinationChaseAction(agent),
            new TrySetChaseDestinationTask(player, transform.position,minimumDistanceFromTheTarget),
            new CheckChaseHasDestinationTask(),
        };
        pathSequencer = new Sequencer(destinationSequencer);
        Debug.Log("ATTACHING CHILDREN TO ROOT NODE");
        _rootNode.AttachChild(pathSequencer);
        Debug.Log($"ROOT NODE CHILDREN COUNT => {_rootNode.Children.Count}");
        return _rootNode;
    }
}
