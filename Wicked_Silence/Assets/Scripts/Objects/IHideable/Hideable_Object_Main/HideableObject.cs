using UnityEngine;
//BASE CLASS...
[RequireComponent(typeof (Rigidbody))]
public class HideableObject : MonoBehaviour, IHideable {
    [Header("Hideable Object Data")]
    public HideableObjectData hideableObjectData;
    [SerializeField] private int hideableObjectLayer = 8;
    [Header("Needed References")]
    public PlayerMovement player;
    public HidingCamera hidingCamera;
    public Camera mainCamera;
    bool hiding;
    #region Hideable Objects
    Closet closet;
    Locker locker;
    Desk desk;
    #endregion
    public HideableObject(HideableObjectData hideableObjectData, PlayerMovement player, HidingCamera hidingCamera, Camera mainCamera) {
        this.hideableObjectData = hideableObjectData;
        this.player = player;
        this.hidingCamera = hidingCamera;
        this.mainCamera = mainCamera;
    }
    private void OnEnable() {
        InitializeObjects();
        RigidbodyInitialization();
        hiding = GetComponent<IHideable>().IsHiding();
        hideableObjectData.IsHiding = hiding;
    }
    //Deciding which object is this & attaching the right component that corresponds 
    //to the name of the HideableObject...
    #region Object Initialization
    void InitializeObjects() {
        switch (hideableObjectData.name) {
            case ("Closet"): {
                this.gameObject.AddComponent<Closet>();
                closet = gameObject.GetComponent<Closet>();
                break;
            }
            case ("Locker"): {
                this.gameObject.AddComponent<Locker>();
                locker = gameObject.GetComponent<Locker>();
                break;
            }
            case ("Desk"): {
                this.gameObject.AddComponent<Desk>();
                desk = gameObject.GetComponent<Desk>();
                break;
            }
        }
    }
    #endregion 
    #region Choose Hide Or Exit Hide Action
    //Choose which action to take based on whether the player in hiding...
    public void ChooseHideAction() { DecideObjectHideOrExit(hiding); }
    #endregion
    //Get this objects data methods...
    #region Get Information & Data
    public virtual bool IsHiding() { return hiding; }
    public HideableObjectData GetObjectData() { return this.hideableObjectData; }
    #endregion

    #region Decide Objects & Invoke the right action
    //Give the methods & statements to decide which action to take...
    public void InvokeHideOrExit(IHideable objectToHide, bool IsHiding) {
        if (!IsHiding) {
            hiding = true;
            objectToHide.Hide();
        }
        else {
            hiding = false; 
            objectToHide.ExitHide();
        }
    }
    //Decide what object should invoke the corresponding methods...
    public void DecideObjectHideOrExit(bool IsHiding) {
        switch (hideableObjectData.name) {
            case ("Closet"): {
                InvokeHideOrExit(closet, IsHiding);
                break;
            }
            case ("Locker"): {
                InvokeHideOrExit(locker, IsHiding);
                break;
            }
            case ("Desk"): {
                InvokeHideOrExit(desk, IsHiding);
                break;
            }
        }
    }
    #endregion
    #region HideableObject Methods & Functionality
    //Hiding Camera & Main Camera Activation/Deactivation...
    public void CameraActivation(bool open){
        this.hidingCamera.gameObject.SetActive(open);
        mainCamera.gameObject.SetActive(!open);
    }
    public virtual void Initialization() {
        HideableObject hideableObjectReference = gameObject.GetComponent<HideableObject>();
        hideableObjectData = hideableObjectReference.GetObjectData();
        player = hideableObjectReference.player;
        hidingCamera = hideableObjectReference.hidingCamera;
        mainCamera = hideableObjectReference.mainCamera;
    }
    void RigidbodyInitialization() {
        this.gameObject.layer = hideableObjectLayer;
        if (!gameObject.GetComponent<Rigidbody>().isKinematic) {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public virtual void Hide() {
        hideableObjectData.IsHiding = true;
        player.detectionLevel = PlayerMovement.DetectionLevel.unDetectable;
        CameraActivation(hideableObjectData.IsHiding);
        Debug.Log($"[HIDING IN {hideableObjectData.name}]...");
    }

    public virtual void ExitHide()
    {
        hideableObjectData.IsHiding = false;
        player.detectionLevel = PlayerMovement.DetectionLevel.mediumDiscretion;
        CameraActivation(hideableObjectData.IsHiding);
        Debug.Log("[STOPPED HIDING]...");
    }
    #endregion 
}