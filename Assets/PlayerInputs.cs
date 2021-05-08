using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private static PlayerControls controls;
    static private InputActionAsset inputActions;

    private static float moveInput;

    private void Awake()
    {
        controls = new PlayerControls();

        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
    }

    private void Start()
    {
        controls.Enable();
    }

    public void JumpPerformed(InputAction.CallbackContext context)
    {
        if(PlayerState.GetState() != PlayerState.State.Dead)
        {
            if(context.action.phase == InputActionPhase.Started)
            {
                if(PlayerState.GetIsGrounded())
                {
                    PlayerState.SetState(PlayerState.State.Jumping);                
                }
            }
        }
    }

    public void MovePerformed(InputAction.CallbackContext context)
    {
        if(PlayerState.GetState() == PlayerState.State.Dead || PlayerState.GetState() == PlayerState.State.Sliding)
        {
            return;
        }

        Vector2 vectorValue = context.ReadValue<Vector2>();

        moveInput = vectorValue.x;

        MoveInput();
    }

    public static void MovePerformedGameplay()
    {
        if(PlayerState.GetState() == PlayerState.State.Dead)
        {
            return;
        }
        
        Vector2 vectorValue = controls.Gameplay.Move.ReadValue<Vector2>();

        moveInput = vectorValue.x;
        
        MoveInput();
    }

    public static void MoveInput()
    {
        if((Math.Abs(moveInput) > 0.01f))
        {
            PlayerState.SetState(PlayerState.State.Moving);
        }
        else
        {
            PlayerState.SetState(PlayerState.State.Idle);
        }
    }

    public void SlidePerformed(InputAction.CallbackContext context)
    {
        if(PlayerState.GetIsGrounded())
        {
            PlayerState.SetState(PlayerState.State.Sliding);
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
