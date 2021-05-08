using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private static float moveInput;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void MoveInput()
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
}
