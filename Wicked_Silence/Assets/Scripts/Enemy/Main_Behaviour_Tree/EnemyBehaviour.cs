using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using BehaviourTree;
using FMODUnity;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(StudioEventEmitter))]
public class EnemyBehaviour : NodeTree {
    NavMeshAgent agent;
    [SerializeField] float m_Range;
    [Header("Enemy Sensor Configs")]
    [SerializeField] private float minDistanceBetween = 50.0f;
    [SerializeField] private float minInvestigationDistance = 40.0f;
    [SerializeField] private float minCaughtPlayerDistance = 10.0f;
    [Tooltip("Microphone Input, Volume & DB Minimum Levels")]
    [SerializeField] private float minInvestigateDBLevel = -60.0f;
    [Tooltip("Microphone Input, Volume & DB Minimum Levels")]
    [SerializeField] private float minChaseDBLevel = -40.0f;
    [Header("Time For Player To Get Away (In Seconds)")]
    [SerializeField] float timeToWait;
    [Header("Minimum Attack Distance")]
    [SerializeField] float minimumAttackDistance;
    public override void Initialize()  {
        agent = GetComponent<NavMeshAgent>();
        base.Initialize();
    }
    /// <summary>
    /// FIX THIS !!!!!!!!!
    /// </summary>
    #region Setting Up Enemy Behaviour Tree
    //Our parallel takes a sequence of two children, check if we have a destination & Go to destination
    //If both of them are qualified as SUCCEEDED then we are going to move to the specified location...
    protected override Node SetUpTree() {
        Debug.Log("SETTING UP ROOT NODE...");
        _rootNode = new Parallel();
        //First Two Children...
        Node SetTargetNodeSensor = new ListenSensor(transform.position, minDistanceBetween, minInvestigationDistance,
            minCaughtPlayerDistance, minInvestigateDBLevel, minChaseDBLevel);
        Node TraverseSelector;
        //THIS SELECTOR (TraverseSelector) (TraverseSelector) WILL HAVE ! 2 ! SEQUENCERS
        // [ SEQUENCER 1 ] : A. CHECK IF TIME PASSED FROM BEING DETECTED < THE SPECIFIED TIME,  B. CHECK IF HAS PLAYER POSITION, C. A SELECTOR WITH 2 CHILDREN :
        //i) GO TO PLAYER POSITION or ii) Kill Player...
        // [ SEQUENCER 2 ] : A. SET SEARCH AREA DESTINATION, B. CHECK IF HAS AREA DESTINATION, C. GO TO POINT IN AREA
        //Selector's First Child (Search Sequencer)
        Node SearchAreaSequencer;
        //Selector's Second Child (Chase Sequencer)
        Node FindAndMoveToPlayerSequencer;
        //Setting up the child list for chase/move to player destination sequencer...
        Node chaseOrAttackSelector = new Selector(new List<Node>() {
                new GoToDestinationChaseAction(agent),
                new AttackPlayerAction(agent, minimumAttackDistance)
            });
        List<Node> destinationToPlayerList = new List<Node>() {
             chaseOrAttackSelector,
             new CheckChaseHasDestinationTask(),
             new Timer(timeToWait),
            
        };
        List<Node> searchAreaSequencer = new List<Node>() {
            new GoToAreaPointAction(agent),
            new TrySetPointInAreaTask(agent, m_Range),
            new CheckHasPointInAreaTask(),
        };
        //Initializing the sequencer...
        FindAndMoveToPlayerSequencer = new Sequencer(destinationToPlayerList);
        SearchAreaSequencer = new Sequencer(searchAreaSequencer);
        //Adding to the selector the first child to evaluate...
        TraverseSelector = new Selector(new List<Node>() {
            SearchAreaSequencer,
            FindAndMoveToPlayerSequencer
        });
        Debug.Log("ATTACHING CHILDREN TO ROOT NODE");
        _rootNode.AttachChild(TraverseSelector);
        _rootNode.AttachChild(SetTargetNodeSensor);
        Debug.Log($"ROOT NODE CHILDREN COUNT => {_rootNode.Children.Count}");
        return _rootNode;
    }
    #endregion
}