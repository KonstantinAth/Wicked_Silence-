using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;
public class AttackPlayerAction : Node {
    GameManager managerInstance => GameManager._instance;
    NavMeshAgent agent;
    float minAttackDistance;
    public AttackPlayerAction(NavMeshAgent agent, float minAttackDistance) : base() {
        this.agent = agent;
        this.minAttackDistance = minAttackDistance;
    }
    #region Attack player if they are on sight
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => ATTACK PLAYER ACTION");
            float distance = Vector3.Distance(agent.transform.position, managerInstance.player.transform.position);
            if(distance <= minAttackDistance) {
                Debug.Log("ATTACKING PLAYER");
                _state = NodeState.SUCCEEDED;
                return _state;
            }
        _state = NodeState.FAILED;
        return _state;
    }
    #endregion
}
