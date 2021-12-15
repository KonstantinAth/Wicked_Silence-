using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using BehaviourTree;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : NodeTree {
    NavMeshAgent agent;
    [SerializeField] float m_Range;
    [SerializeField] private float minDistanceBetween = 50.0f;
    [SerializeField] private float minInvestigationDistance = 40.0f;
    [SerializeField] private float minChaseDistance = 30.0f;
    [SerializeField] private float minCaughtPlayerDistance = 10.0f;
    [SerializeField] private float minInvestigateDBLevel = -60.0f;
    [SerializeField] private float minChaseDBLevel = -40.0f;
    public override void Initialize()  {
        agent = GetComponent<NavMeshAgent>();
        base.Initialize();
    }
    //Our parallel takes a sequence of two children, check if we have a destination & Go to destination
    //If both of them are qualified as SUCCEEDED then we are going to move to the specified location...
    protected override Node SetUpTree() {
        Debug.Log("SETTING UP ROOT NODE...");
        _rootNode = new Parallel();
        //First Two Children...
        Node SetTargetNodeSensor = new ListenSensor(transform.position, minDistanceBetween, minInvestigationDistance,
            minCaughtPlayerDistance, minInvestigateDBLevel, minChaseDBLevel);
        //THIS SELECTOR (TraverseSelector) WILL HAVE ! 2 ! SEQUENCERS
        // [ SEQUENCER 1 ] : A. CHECK IF TIME PASSED FROM BEING DETECTED < THE SPECIFIED TIME,  B. CHECK IF HAS PLAYER POSITION, C. A SELECTOR WITH 2 CHILDREN :
        //i) GO TO PLAYER POSITION or ii) Kill Player...
        // [ SEQUENCER 2 ] : A. SET SEARCH AREA DESTINATION, B. CHECK IF HAS AREA DESTINATION, C. GO TO POINT IN AREA
        Node TraverseSelector;
        //Selector's Second Child (Chase Sequencer)
        Node DestinationSequencer;
        //Setting up the child list for sequencer with the right nodes (children)...
        List<Node> destinationSequencer = new List<Node>() {
            new GoToDestinationChaseAction(agent),
            new CheckChaseHasDestinationTask(),  
        };
        //Initializing the sequencer...
        DestinationSequencer = new Sequencer(destinationSequencer);
        //Adding to the selector the first child to evaluate...
        TraverseSelector = new Selector(new List<Node>() {
            DestinationSequencer
        });
        Debug.Log("ATTACHING CHILDREN TO ROOT NODE");
        _rootNode.AttachChild(TraverseSelector);
        _rootNode.AttachChild(SetTargetNodeSensor);
        Debug.Log($"ROOT NODE CHILDREN COUNT => {_rootNode.Children.Count}");
        return _rootNode;
    }
}
