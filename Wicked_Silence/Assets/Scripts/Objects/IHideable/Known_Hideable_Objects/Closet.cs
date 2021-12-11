using UnityEngine;

public class Closet : HideableObject {
    public Closet(HideableObjectData hideableObjectData, PlayerMovement player, HidingCamera hidingCamera, Camera mainCamera) :
        base (hideableObjectData, player, hidingCamera, mainCamera) {
        this.hideableObjectData = hideableObjectData;
        this.player = player;
        this.hidingCamera = hidingCamera;
        this.mainCamera = mainCamera;
    }
    private void OnEnable() {
        Initialization();
    }
    public override void Initialization() {
        base.Initialization();
    }
    public override void Hide() {
        base.Hide();
    }
    public override void ExitHide() {
        base.ExitHide();
    }
    public override bool IsHiding() {
        return hideableObjectData.IsHiding;
    }
}