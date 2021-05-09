using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerInputs : MonoBehaviour
{
    private PlayableDirector director;

    private GameController gameController;

    private static PlayerControls controls;

    static private InputActionAsset inputActions;

    private static float moveInput;

    private TrackAsset moveLeftTrack;

    private TrackAsset moveRightTrack;

    private TrackAsset jumpTrack;
    
    private TrackAsset slideTrack;

    private TimelineClip currentMoveLeftClip;

    private TimelineClip currentMoveRightClip;

    public float timer;

    private void Awake()
    {
        director = GameObject.Find("Player").GetComponent<PlayableDirector>();

        gameController = FindObjectOfType<GameController>();

        controls = new PlayerControls();

        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;

        TimelineAsset replay = (TimelineAsset)GameObject.Find("Player").GetComponent<PlayableDirector>().playableAsset;
        
        moveLeftTrack = replay.GetOutputTrack(1);

        moveRightTrack = replay.GetOutputTrack(2);

        jumpTrack = replay.GetOutputTrack(3);

        slideTrack = replay.GetOutputTrack(4);
    }

    private void Start()
    {
        controls.Enable();
    }

    private void Update()
    {
        if(timer < gameController.currentSequenceDuration)
        {
            timer += Time.deltaTime;
        }
    }

    public void FinaliseClips()
    {
        if(currentMoveLeftClip != null)
        {
            currentMoveLeftClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveLeftClip.start)));
            
            currentMoveLeftClip = null;
        }
        
        if(currentMoveRightClip != null)
        {
            currentMoveRightClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveRightClip.start)));
            
            currentMoveRightClip = null;
        }
    }

    public void JumpPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            TimelineClip clip = jumpTrack.CreateDefaultClip();

            clip.duration = 0.1f;

            clip.start = timer;
        }
    }

    public void SlidePerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            TimelineClip clip = slideTrack.CreateDefaultClip();

            clip.duration = 0.1f;

            clip.start = timer;
        }
    }
    
    public void LeftPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            currentMoveLeftClip = moveLeftTrack.CreateDefaultClip();

            currentMoveLeftClip.start = timer;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            if(currentMoveLeftClip == null && gameController.inInputPhase)
            {
                currentMoveLeftClip = moveLeftTrack.CreateDefaultClip();

                currentMoveLeftClip.start = 0;
            }

            currentMoveLeftClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveLeftClip.start)) + (gameController.currentSequenceDuration - timer));
        
            currentMoveLeftClip = null;
        }
    }
    
    public void RightPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            currentMoveRightClip = moveRightTrack.CreateDefaultClip();
            
            currentMoveRightClip.start = timer;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            if(currentMoveRightClip == null && gameController.inInputPhase)
            {
                currentMoveRightClip = moveRightTrack.CreateDefaultClip();

                currentMoveRightClip.start = 0;
            }

            currentMoveRightClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveRightClip.start)) + (gameController.currentSequenceDuration - timer));

            currentMoveRightClip = null;    
        }
    }

    public static float GetMoveInput()
    {
        if(Mathf.Abs(moveInput) > 0.01f)
        {
            return moveInput;
        }
        else
        {
            return 0f;
        }
    }

    public static InputActionAsset GetInputActions()
    {
        return inputActions;
    }

    public static void SetMoveInput(float moveInput)
    {
        PlayerInputs.moveInput = moveInput;
    }
}
