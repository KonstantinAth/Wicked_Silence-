using UnityEngine;
using TMPro;
public class Closet : MonoBehaviour, IHideable {
    [System.Serializable]
    public struct HideableObjectData {
        public string name;
        public string type;
        public Transform position;
        public bool IsHiding;
    }
    public HideableObjectData objectData;
    private void OnEnable() {
        objectData.IsHiding = false;
    }
    // Update is called once per frame
    void Update() {
        
    }
    public void Hide() {
        objectData.IsHiding = true;
        Debug.Log("[HIDING]...");
    }
    public void ExitHide() {
        objectData.IsHiding = false;
        Debug.Log("[STOPPED HIDING]...");
    }
    public bool IsHiding() {
        return objectData.IsHiding;
    }
    public HideableObjectData GetObjectData() {
        return objectData;
    }
}
