using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace BehaviourTree {
    public class Node : IBehaviourTreeNode {
        //Node's running state...
        public enum NodeState {
            RUNNING,
            FAILED,
            SUCCEEDED
        }
        #region Accessors
        protected NodeState _state;
        public NodeState State { get => _state; }
        #endregion
        //Each Node's parent & children nodes...
        private Node _parent;
        protected List<Node> children = new List<Node>();
        //data Values hold the data value/s of each node
        private Dictionary<string, object> _objectData = new Dictionary<string, object>();  
        #region Constructors
        public Node() { _parent = null; }
        public Node(List<Node> children) : this() {
            SetChildren(children);
        }
        #endregion
        #region Interface Utils
        public virtual NodeState Evaluate() => NodeState.FAILED;
        public virtual void ResetNode() { }
        #endregion
        #region Initialize Node & Handle Children Nodes
        //Set node's children nodes...
        public void SetChildren(List<Node> children) {
            foreach (Node child in children) {
                AttachChild(child);
            }
        }
        //Attach a child node to the current node...
        public void AttachChild(Node child) {
            children.Add(child);
            child._parent = this;
        }
        //Remove a child node from the current node...
        public void DetachChild(Node child) {
            children.Remove(child);
            child._parent = null;
        }
        #endregion
        #region Handle Object's Data
        //Get objects's data...
        public object GetData(string Key) {
            object value = null;
            if (_objectData.TryGetValue(Key, out value)) {
                return value;
            }
            Node node = _parent;
            while (node != null) {
                value = node.GetData(Key); 
                if (value != null) {
                    return value;
                }
                node = node._parent;
            }
            return null;
        }
        //Clear object's data...
        public bool ClearData(string Key) {
            if(_objectData.ContainsKey(Key)){
                _objectData.Remove(Key);
                return true;
            }
            Node node = _parent;
            while(node != null) {
                bool cleared = node.ClearData(Key);
                if(cleared) {
                    return true;
                }
                node = node._parent;
            }
            return false;
        }
        //Set an object's data...
        public void SetData(string Key, object Value) {
            _objectData[Key] = Value;
        }
        #endregion
        #region Accessors => Parent, Children, HasChildren
        public Node Parent { get => _parent; }
        public List<Node> Children { get => children; }
        public bool HasChildren { get => children.Count > 0; }
        #endregion
    }
}