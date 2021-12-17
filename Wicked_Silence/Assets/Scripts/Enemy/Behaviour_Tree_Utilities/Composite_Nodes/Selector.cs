using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    //Selector decides which child to execute based on an evaluation...
    public class Selector : Node {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        //Evaluate each child's state & return it...
        public override NodeState Evaluate() {
            Debug.Log("EVALUATING => SELECTOR NODE");
            foreach (Node node in children) {
                switch (node.Evaluate()) {
                    case NodeState.FAILED: {
                        Debug.Log("SELECTOR NODE => FAILED");
                        break;
                        
                    }
                    case NodeState.RUNNING: {
                        Debug.Log("SELECTOR NODE => RUNNING");
                        _state = NodeState.RUNNING;
                        return _state;
                    }
                    case NodeState.SUCCEEDED: {
                        Debug.Log("SELECTOR NODE => SUCCEEDED");
                        _state = NodeState.SUCCEEDED;
                        return _state;
                    }
                    //If none of the above is valid continue...
                    default: {
                        break;
                    }
                }
            }
            //In any other case return node's condition failed...
            _state = NodeState.FAILED;
            return _state;
        }
    }
}
