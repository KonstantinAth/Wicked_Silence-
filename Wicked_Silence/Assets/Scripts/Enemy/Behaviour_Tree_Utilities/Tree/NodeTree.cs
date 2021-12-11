using UnityEngine;
namespace BehaviourTree {
    //Our behaviour tree, which will be filled with all the needed nodes...
    //It's abstract because we still do not have a definition of the tree...
    //But we still want to access the tree from the derived classes...
    public abstract class NodeTree : MonoBehaviour {
        protected Node _rootNode = null;
        protected void Start() { Initialize(); }
        protected void Update() { Evaluate(); }
        public virtual void Initialize() {
            Debug.Log("SETTING UP NODE TREE...");
            SetUpTree();
        }
        public void Evaluate() {
            if (_rootNode != null) {
                Debug.Log("EVALUATING ROOT NODE");
                _rootNode.Evaluate();
            }
        }
        public Node Root => _rootNode;
        protected abstract Node SetUpTree();
    }
}