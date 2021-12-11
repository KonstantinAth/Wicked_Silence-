using System;
using UnityEngine;
using BehaviourTree;
//Try to set a destination for the agent to go to...
public class TrySetChaseDestinationTask : Node {
    Vector3 currentObjectTransform;
    PlayerMovement player;
    float minimumDistance;
    public TrySetChaseDestinationTask(PlayerMovement player, Vector3 currentObjectTransform, float minimumDistance) : base() {
        this.currentObjectTransform = currentObjectTransform;
        this.player = player;
        this.minimumDistance = minimumDistance;
    }
    //To be used...
    public Tuple<bool, Vector3> FindPath() {
        throw new NotImplementedException(); 
    }
    public override NodeState Evaluate() {
        Debug.Log("EVALUATING => [TRY SET DESTINATION TASK]");
        float distance = Vector3.Distance(currentObjectTransform, player.gameObject.transform.position);
        switch (player.detectionLevel) {
            //If player is hiding (Undetectable)
            case PlayerMovement.DetectionLevel.unDetectable: {
                    //Go to player's last known position...
                if(player.HasLastKnowPosition()) {
                    Parent.SetData("Destination", PlayerMovement.LastKnownPosition);
                    _state = NodeState.SUCCEEDED;
                        Debug.Log("TRY SET DESTINATION SUCCEEDED");
                        return _state;
                }
                //is player has not a last know position keep running until you find a destiantion...
                else {
                    _state = NodeState.RUNNING;
                        Debug.Log("TRY SET DESTINATION RUNNING");
                        return _state;
                }
            }
            //FIX THIS !!!
            //If player is walking (medium )
            //I NEED TO FIX THIS WITH A TIMER DECORATOR, WHEN PLAYER IS LOUD GIVE TIME TO THE AGENT TO ISPECT THE SOUND SOURCE...
            case PlayerMovement.DetectionLevel.mediumDiscretion: {
                //If we have last know position & the micloudness is "quite" & player is far go to the last known position...
                if (player.HasLastKnowPosition() && !MicrophoneInput.HasLastKnowPosition()) {
                    Parent.SetData("Destination", PlayerMovement.LastKnownPosition);
                    _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
                }
                ////If the player is loud & close go to player
                else if (MicrophoneInput.HasLastKnowPosition() && distance <= minimumDistance) {
                    Parent.SetData("Destination", player.transform.position);
                    //I MUST ALSO RECEIVE LAST KNOWN POSITION VALUES FROM THE MICROPHONE INPUT SCRIPT, WHEN THE PLAYER IS LOUD...
                    _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
                }
                //if player was loud, but further away go to the last know position he was loud... 
                else if (MicrophoneInput.HasLastKnowPosition()  && distance > minimumDistance) {
                    Parent.SetData("Destination", MicrophoneInput.LastKnownPosition);
                    _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
                }
                else {
                    _state = NodeState.RUNNING;
                    Debug.Log("TRY SET DESTINATION RUNNING");
                    return _state;
                }
            }
                //if player is discrete (crouching)
            case PlayerMovement.DetectionLevel.discrete: {
                    //Go to the last known position he was loud...
                if (player.HasLastKnowPosition() && !MicrophoneInput.HasLastKnowPosition()) {
                    Parent.SetData("Destination", PlayerMovement.LastKnownPosition);
                    _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
                }
                else if(MicrophoneInput.HasLastKnowPosition()) {
                    Parent.SetData("Destination", MicrophoneInput.LastKnownPosition);
                    _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
                }
                else {
                    _state = NodeState.RUNNING;
                        Debug.Log("TRY SET DESTINATION RUNNING");
                        return _state;
                }
            }
                //If player is loud go to player's position...
            case PlayerMovement.DetectionLevel.loud: {
                Parent.SetData("Destination", player.gameObject.transform.position);
                _state = NodeState.SUCCEEDED;
                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
                    return _state;
            }
        }
        //if none of the above is valid the node state of the node has failed...
        _state = NodeState.FAILED;
        Debug.Log("TRY SET DESTINATION FAILED");
        return _state;
    }
}