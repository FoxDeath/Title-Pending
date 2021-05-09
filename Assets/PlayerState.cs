using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerState : MonoBehaviour
{

    static private PlayerMovement playerMovement;
    static private PlayerInputs playerInputs;
    static private PlayerAnimatorController playerAnimationController;
    static private PlayerAudioController playerAudioController;


    static private State state;
    public enum State
    {
        Idle,
        Moving,
        Jumping,
        Falling,
        Sliding,
        Dead
    }

    static private bool isFacingRight;
    static private bool isGrounded;

    private void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();

        playerInputs = GetComponent<PlayerInputs>();
        playerAnimationController = GetComponent<PlayerAnimatorController>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }

    private void Start()
    {
        state = State.Idle;   

        isFacingRight = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(state == State.Dead)
        {
            return;
        }

        switch(state)
        {
            case State.Moving:
            break;

            case State.Jumping:
                if(GetIsGrounded() && PlayerMovement.GetVelocity().y <= 0f)
                {
                    SetState(State.Idle);
                }
            break;

            case State.Falling:
            break;
            
            case State.Sliding:
            break;

            case State.Dead:
            break;

            case State.Idle:
            break;
        }
    }

    private void FixedUpdate()
    {
        if(state == State.Dead)
        {
            return;
        }

        switch(state)
        {
            case State.Moving:
                playerMovement.Gravity();

                playerMovement.VelocityClamp();
            break;

            case State.Jumping:
                playerMovement.VelocityClamp();
            break;

            case State.Falling:
                playerMovement.Gravity();

                playerMovement.VelocityClamp();

                if(isGrounded)
                {
                    SetState(State.Idle);
                }
            break;
            
            case State.Sliding:
                playerMovement.Gravity();
            break;

            case State.Dead:    
                playerMovement.Gravity();

                playerMovement.VelocityClamp();
            break;

            case State.Idle:
                playerMovement.Gravity();

                playerMovement.VelocityClamp();
            break;
        }
    }

    static public void SetState(State newState)
    {
        if(state == State.Dead)
        {
            return;
        }

        switch(newState)
        {
            case State.Moving:
                if(state != State.Moving && state != State.Jumping && state != State.Sliding)
                {
                    playerAudioController.SetMusicParameter("Slide", 0f);

                    playerAudioController.SetMusicParameter("Movement", 1f);

                    ResetAllTriggers();

                    playerAnimationController.SetTrigger("Moving");

                    state = newState;
                }
            break;

            case State.Jumping:
                if(state != State.Jumping && state != State.Sliding)
                {
                    playerAudioController.SetMusicParameter("Jump", 1f);

                    state = newState;

                    ResetAllTriggers();

                    playerAnimationController.SetTrigger("Jumping");
                }
                break;

            case State.Falling:
                if(PlayerState.state != State.Falling && PlayerMovement.GetVelocity().y <= 0f)
                {
                    playerAudioController.SetMusicParameter("Jump", 0f);

                    ResetAllTriggers();

                    playerAnimationController.SetTrigger("Falling");

                    state = newState;
                }
            break;
            
            case State.Sliding:
                if(state != State.Sliding && state != State.Jumping)
                {
                    playerAudioController.SetMusicParameter("Slide", 1f);

                    ResetAllTriggers();

                    playerAnimationController.SetTrigger("Sliding");

                    state = newState;
                }
            break;

            case State.Dead:
                    playerAudioController.ResetParamaters();

                state = newState;
            break;

            case State.Idle:
                if(state != State.Idle)
                {
                    playerAudioController.ResetParamaters();

                    ResetAllTriggers();

                    playerAnimationController.SetTrigger("Idle");

                    state = newState;

                    PlayerMovement.StopMoving();
                }
            break;
        }
    }

    private static void ResetAllTriggers()
    {
        playerAnimationController.ResetTrigger("Moving");
        playerAnimationController.ResetTrigger("Jumping");
        playerAnimationController.ResetTrigger("Falling");
        playerAnimationController.ResetTrigger("Sliding");
        playerAnimationController.ResetTrigger("Idle");
    }

    static public void SetIsFacingRight(bool isFacingRight)
    {
        if(PlayerState.isFacingRight != isFacingRight)
        {
            PlayerState.isFacingRight = isFacingRight;
        }
    }

    static public void SetIsGrounded(bool isGrounded)
    {
        if(PlayerState.isGrounded != isGrounded)
        {
            PlayerState.isGrounded = isGrounded;
        }
    }

    static public State GetState()
    {
        return state;
    }

    static public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    static public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
