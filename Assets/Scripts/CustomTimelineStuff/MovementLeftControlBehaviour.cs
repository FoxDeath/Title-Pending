using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class MovementLeftControlBehaviour : PlayableBehaviour
{
    private PlayerMovement movementController;
    
    private bool firstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        movementController = playerData as PlayerMovement;

        movementController.Movement(-1f);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        firstFrameHappened = false;

        if(movementController == null)
        {
            return;
        }

        //set character to default state
        PlayerState.SetState(PlayerState.State.Idle);

        base.OnBehaviourPause(playable, info);
    }
}
