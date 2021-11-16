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
    [SerializeField] PlayerMovement player;
    private void OnEnable() {
        objectData.IsHiding = false;
    }
    // Update is called once per frame
    void Update() {
        
    }
    public void Hide() {
        objectData.IsHiding = true;
        player.detectionLevel = PlayerMovement.DetectionLevel.unDetectable;
        Debug.Log("[HIDING]...");
    }
    public void ExitHide() {
        objectData.IsHiding = false;
        player.detectionLevel = PlayerMovement.DetectionLevel.mediumDiscretion;
        Debug.Log("[STOPPED HIDING]...");
    }
    public bool IsHiding() {
        return objectData.IsHiding;
    }
    public HideableObjectData GetObjectData() {
        return objectData;
    }
}
