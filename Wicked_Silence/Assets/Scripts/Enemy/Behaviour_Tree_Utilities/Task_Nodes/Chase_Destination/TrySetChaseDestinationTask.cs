using System;
using UnityEngine;
using BehaviourTree;
//PROBABLY WRONG APPROACH...

////Try to set a destination for the agent to go to...
//public class TrySetChaseDestinationTask : Node {
//    Vector3 currentObjectTransform;
//    float minimumDistance;
//    GameManager GameManagerinstance => GameManager._instance;
//    public TrySetChaseDestinationTask(Vector3 currentObjectTransform, float minimumDistance) : base() {
//        this.currentObjectTransform = currentObjectTransform;
//        this.minimumDistance = minimumDistance;
//    }
//    public override NodeState Evaluate() {
//        Debug.Log("EVALUATING => [TRY SET DESTINATION TASK]");
//        float distance = Vector3.Distance(currentObjectTransform, GameManagerinstance.player.gameObject.transform.position);
//        switch (GameManagerinstance.player.detectionLevel) {
//            //If player is hiding (Undetectable)
//            case PlayerMovement.DetectionLevel.UNDETECTABLE: {
//                //Go to player's last known position...
//                if (GameManagerinstance.player.HasLastKnowPosition()) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.player.LastKnownPosition);
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                //is player has not a last know position keep running until you find a destiantion...
//                else {
//                    _state = NodeState.RUNNING;
//                    Debug.Log("TRY SET DESTINATION RUNNING");
//                    return _state;
//                }
//            }
//            //FIX THIS !!!
//            //If player is walking (medium )
//            //I NEED TO FIX THIS WITH A TIMER DECORATOR, WHEN PLAYER IS LOUD GIVE TIME TO THE AGENT TO ISPECT THE SOUND SOURCE...
//            case PlayerMovement.DetectionLevel.MEDIUMDISCRETION: {
//                //If we have last know position & the micloudness is "quite" & player is far go to the last known position...
//                if (GameManagerinstance.player.HasLastKnowPosition() && !GameManagerinstance.micInput.HasLastKnowPosition()) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.player.LastKnownPosition);
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                ////If the player is loud & close go to player
//                else if (GameManagerinstance.micInput.HasLastKnowPosition() && distance <= minimumDistance) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.player.transform.position);
//                    //I MUST ALSO RECEIVE LAST KNOWN POSITION VALUES FROM THE MICROPHONE INPUT SCRIPT, WHEN THE PLAYER IS LOUD...
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                //if player was loud, but further away go to the last know position he was loud... 
//                else if (GameManagerinstance.micInput.HasLastKnowPosition() && distance > minimumDistance) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.micInput.LastKnownPosition);
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                else {
//                    _state = NodeState.RUNNING;
//                    Debug.Log("TRY SET DESTINATION RUNNING");
//                    return _state;
//                }
//            }
//            //if player is discrete (crouching)
//            case PlayerMovement.DetectionLevel.DISCRETE: {
//                //Go to the last known position he was loud...
//                if (GameManagerinstance.player.HasLastKnowPosition() && !GameManagerinstance.micInput.HasLastKnowPosition()) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.player.LastKnownPosition);
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                else if (GameManagerinstance.micInput.HasLastKnowPosition()) {
//                    Parent.SetData("PlayerDestination", GameManagerinstance.micInput.LastKnownPosition);
//                    _state = NodeState.SUCCEEDED;
//                    Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                    return _state;
//                }
//                else {
//                    _state = NodeState.RUNNING;
//                    Debug.Log("TRY SET DESTINATION RUNNING");
//                    return _state;
//                }
//            }
//            //If player is loud go to player's position...
//            case PlayerMovement.DetectionLevel.LOUD: {
//                Parent.SetData("PlayerDestination", GameManagerinstance.player.transform.position);
//                _state = NodeState.SUCCEEDED;
//                Debug.Log("TRY SET DESTINATION SUCCEEDED");
//                return _state;
//            }
//        }
//        //if none of the above is valid the node state of the node has failed...
//        _state = NodeState.FAILED;
//        Debug.Log("TRY SET DESTINATION FAILED");
//        return _state;
//    }
//}