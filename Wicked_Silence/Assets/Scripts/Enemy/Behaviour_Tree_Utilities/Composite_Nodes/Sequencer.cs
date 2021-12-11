using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//Sequencer executes a sequence of a node's actions...
//Sequencer's child nodes must all meet the condition given when evaluated ! 
namespace BehaviourTree {
    public class Sequencer : Node {
        public Sequencer() : base() { }
        public Sequencer(List<Node> children) : base(children) { }
        public override NodeState Evaluate() {
            Debug.Log("EVALUATING => [SEQUENCER]");
            Debug.Log($"Sequencer children count : {children.Count}");

            bool isAnyChildRunning = false;
            foreach (Node child in children) {
                switch (child.Evaluate()) {
                    //If at least one child failed, the sequencer has failed...
                    case NodeState.FAILED: {
                        Debug.Log("SEQUENCER FAILED");
                        _state = NodeState.FAILED;
                        return _state;
                    }
                    //If a child is running continue until it has succeeded...
                    case NodeState.RUNNING: {
                            Debug.Log("SEQUENCER RUNNING");
                            isAnyChildRunning = true;
                        continue;
                    }
                    //If the child succeeded declare it & return it... 
                    case NodeState.SUCCEEDED: {
                            Debug.Log("SEQUENCER SUCCEEDED");
                            _state = NodeState.SUCCEEDED;
                        return _state;
                    }
                }
            }
            //If there is a child still running we declare it, otherwise we declare success...
            _state = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCEEDED;
            //Return this nodes state...
            return _state;
        }
    }
}
