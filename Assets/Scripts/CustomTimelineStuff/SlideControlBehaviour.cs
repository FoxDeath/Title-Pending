using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class SlideControlBehaviour : PlayableBehaviour
{
    private PlayerMovement movementController;
    
    private bool firstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if(!firstFrameHappened)
        {
            firstFrameHappened = true;

            movementController = playerData as PlayerMovement;

            movementController.Slide();
        }
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
