using System.Collections.Generic;
namespace BehaviourTree
{
    //Selector decides which child to execute based on an evaluation...
    public class Selector : Node {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        //Evaluate each child's state & return it...
        public override NodeState Evaluate() {
            foreach (Node node in children) {
                switch (node.Evaluate()) {
                    case NodeState.FAILED: {
                        continue;
                    }
                    case NodeState.RUNNING: {
                        _state = NodeState.RUNNING;
                        return _state;
                    }
                    case NodeState.SUCCEEDED: {
                        _state = NodeState.SUCCEEDED;
                        return _state;
                    }
                    //If none of the above is valid continue...
                    default: {
                        continue;
                    }
                }
            }
            //In any other case return node's condition failed...
            _state = NodeState.FAILED;
            return _state;
        }
    }
}
