using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree {
    //Proccesses each child at the same time returns the failed/success state of each children...
    //This parallel will succeed if at least one child node is succesful...
    public class Parallel : Node {
        public Parallel() : base() { }
        public Parallel(List<Node> children) : base(children) { }
        public override NodeState Evaluate() {
            Debug.Log("EVALUATING => [PARALLEL]");
            Debug.Log($"[PARALLEL CHILDREN] => {children.Count}");
            bool isAnyChildRunning = false;
            int failedChildren = 0;
            foreach (Node child in children) {
                switch (child.Evaluate()) {
                    //If failed increase the children that have been failed...
                    case NodeState.FAILED: {
                        failedChildren++;
                            Debug.Log($"FAILED CHILDREN {failedChildren}");
                        continue;
                    }
                    //Continue if succeeded..
                    case NodeState.SUCCEEDED: {
                            Debug.Log($"PARALLEL SUCCEEDED");
                            continue;
                    }
                    //There is a child running...
                    case NodeState.RUNNING: {
                            Debug.Log($"PARALLEL RUNNING");
                            isAnyChildRunning = true;
                        continue;
                    }
                    //If any other condition hasn't been met the 
                    //Child succeeded (because it is not running a procedure, so we are not processing it anymore)...
                    default: {
                        _state = NodeState.SUCCEEDED;
                        return _state;
                    }
                }
            }
            //If all children failedm, we failed...
            if (failedChildren == children.Count) { _state = NodeState.FAILED; }
            //If there is any child running the child's state is running, else it has succeeded (because we are not processing it anymore)...
            else { _state = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCEEDED; }
            Debug.Log($"PARALLEL NODE STATE => {_state}");
            return _state;
        }
    }
}