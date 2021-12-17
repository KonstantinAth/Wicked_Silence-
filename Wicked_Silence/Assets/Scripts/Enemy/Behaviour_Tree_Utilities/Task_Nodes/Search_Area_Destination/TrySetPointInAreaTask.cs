using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;
public class TrySetPointInAreaTask : Node {
    private float m_Range = 25.0f;
    private NavMeshAgent agent;
    public TrySetPointInAreaTask(NavMeshAgent agent, float m_Range) : base() {
        this.agent = agent;
        this.m_Range = m_Range;
    }
    /// <summary>
    /// FIX THIS !!!
    /// </summary>
    /// <returns></returns>
    #region Set Up Point Inside A Unit Circle
    public override NodeState Evaluate() {
        if (ListenSensor.detected /*|| !Timer.playerGotAway*/) {
            Debug.Log("SEARCH POINT TASK => FAILED");
            Parent.ClearData("SearchDestination");
            _state = NodeState.FAILED;
            return _state;
        }
        else {
            Debug.Log("SEARCH POINT TASK => SUCCEEDED");
            if(agent.pathPending || agent.remainingDistance > 0.1f) {
                Debug.Log("SEARCH POINT TASK => RUNNING");
                return NodeState.RUNNING;
            }
            Vector3 position = m_Range * Random.insideUnitCircle;
            Parent.SetData("SearchDestination", position);
            _state = NodeState.SUCCEEDED;
            return _state;
        }
    }
    #endregion
    //GIVE POINTS IN THE NAVMESHSURFACE/AREA TO GO TO. 
    //WHEN A POINT IS REACHED CHOOSE ANOTHER & REPEAT...

    //BELOW IS A SIMPLE PATROL ACTION FOR A RANDOM POINT INSIDE A UNIT CIRCLE..
    //NavMeshAgent m_Agent;

    //void Start()
    //{
    //    m_Agent = GetComponent<NavMeshAgent>();
    //}

    //void Update()
    //{
    //    if (m_Agent.pathPending || m_Agent.remainingDistance > 0.1f)
    //        return;

    //    m_Agent.destination = m_Range * Random.insideUnitCircle;
    //}
}