using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHiding : MonoBehaviour {
    PlayerDetectObjects playerDetect;
    private void OnEnable() {
        Initialization();
    }
    // Update is called once per frame
    void Update() {  PlayerHideAction(); }
    void Initialization() { playerDetect = GetComponent<PlayerDetectObjects>(); }
    //We invoke Hide or ExitHide based on the object detection & player's inputs...
    void PlayerHideAction() {
        if (playerDetect.playerWantsToHide) {
            if (playerDetect.GetHitResult() != null) {
                if (!playerDetect.GetHitResult().GetComponent<IHideable>().IsHiding()) {
                    playerDetect.hiding = true;
                    playerDetect.GetHitResult().GetComponent<HideableObject>().ChooseHideAction();
                }
                else {
                    playerDetect.hiding = false;
                    playerDetect.GetHitResult().GetComponent<HideableObject>().ChooseHideAction();
                }
            }
        }
    }
}
