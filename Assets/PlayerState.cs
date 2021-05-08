using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerState : MonoBehaviour
{
    //to remove when building
    private void OnDrawGizmos() 
    {
        Handles.Label(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), state.ToString());
    }

    static private PlayerMovement playerMovement;
    static private PlayerInputs playerInputs;


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
                playerMovement.Movement();

                playerMovement.Gravity();

                playerMovement.VelocityClamp();
            break;

            case State.Jumping:
                playerMovement.Movement();

                PlayerMovement.StopMoving();

                playerMovement.VelocityClamp();
            break;

            case State.Falling:
                playerMovement.Movement();

                playerMovement.Gravity();

                PlayerMovement.StopMoving();

                playerMovement.VelocityClamp();
            break;
            
            case State.Sliding:
                playerMovement.Gravity();
            break;

            case State.Dead:    
                playerMovement.Gravity();

                playerMovement.VelocityClamp();
            break;

            case State.Idle:
                playerMovement.Movement();

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
                if(state != State.Moving)
                {
                    state = newState;
                }
            break;

            case State.Jumping:
                if(state != State.Jumping && state != State.Sliding)
                {
                    state = newState;

                    if(GetIsGrounded())
                    {
                        playerMovement.Jump();
                    }
                }
            break;

            case State.Falling:
                if(PlayerState.state != State.Falling && PlayerMovement.GetVelocity().y <= 0f)
                {
                    state = newState;
                }
            break;
            
            case State.Sliding:
                if(state != State.Sliding && state != State.Jumping)
                {
                    state = newState;

                    playerMovement.Slide();
                }
            break;

            case State.Dead:
                state = newState;
            break;

            case State.Idle:
                if(state != State.Idle)
                {
                    state = newState;

                    PlayerMovement.StopMoving();
                }
            break;
        }
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

            if(isGrounded)
            {
            }
            else
            {
            }
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
