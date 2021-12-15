using UnityEngine.AI;
using BehaviourTree;
public class TrySetPointInAreaTask : Node {
    public TrySetPointInAreaTask() : base() { }
    public float m_Range = 25.0f;
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