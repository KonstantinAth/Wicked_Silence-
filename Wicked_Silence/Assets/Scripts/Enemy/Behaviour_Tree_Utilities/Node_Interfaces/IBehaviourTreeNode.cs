namespace BehaviourTree {
    public interface IBehaviourTreeNode {
        //Evaluate Node's state
        public Node.NodeState Evaluate();
        //Execute block contain's the execution functionality of each node...
        //Reset Node's state to original state (or any other needed state)...
        public void ResetNode();
    }
}