using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] float slideSpeed = 65f;
    [SerializeField] float slideTime = 0.6f;
    [SerializeField] float gravity;

    static public float facing;

    private Coroutine jumpTimerCoroutine;

    private static Rigidbody2D myRigidbody;
    private SpriteRenderer spriteRenderer;

    private ControlsOverlay controlsOverlay = new ControlsOverlay();

    private void Awake() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        controlsOverlay = FindObjectOfType<ControlsOverlay>();
    }

    private void Start() 
    {
        facing = 0;    
    }

    private void Update() 
    {
        Facing();
    }

    private void Facing()
    {
        if(PlayerState.GetIsFacingRight())
        {
            facing = 1;
        }
        else
        {
            facing = -1;
        }
    }

    public void Gravity()
    {
        myRigidbody.velocity += Vector2.down * gravity;
    }

    public void Movement(float direction)
    {
        if(PlayerState.GetState() == PlayerState.State.Sliding)
        {
            return;
        }
        controlsOverlay.SetFaceBar();

        PlayerState.SetState(PlayerState.State.Moving);

        FlipPlayer(direction);

        myRigidbody.velocity += new Vector2(direction * speed, 0f);
    }

    public void Jump()
    {
        if(!PlayerState.GetIsGrounded())
        {
            return;
        }

        PlayerState.SetState(PlayerState.State.Jumping);

        if(jumpTimerCoroutine != null)
        {
            StopCoroutine(jumpTimerCoroutine);
        }

        myRigidbody.velocity += Vector2.up * jumpForce;

        jumpTimerCoroutine = StartCoroutine(JumpTimerBehaviour());

        controlsOverlay.JumpActionCreated(); 
    }

    public void Slide()
    {
        if(!PlayerState.GetIsGrounded() || PlayerState.GetState() == PlayerState.State.Sliding)
        {
            return;
        }
        
        PlayerState.SetState(PlayerState.State.Sliding);

        StartCoroutine(SlideBehaviour());
    }

    public void VelocityClamp(float clampX = 0f, float clampY = 0f)
    {
        if(clampX == 0f)
        {
            clampX = speed;            
        }

        if(clampY == 0f)
        {
            clampY = jumpForce;
        }

        myRigidbody.velocity = new Vector2(Mathf.Clamp(myRigidbody.velocity.x, -clampX, clampX), Mathf.Clamp(myRigidbody.velocity.y, -clampY, clampY));
    }

    static public void StopMoving(bool noInput = false)
    {
        PlayerMovement.SetVelocityX(0f);
    }

    static public void StopJumping(bool whenFalling = false)
    {
        if(PlayerMovement.GetVelocity().y > 0f)
        {
            PlayerMovement.SetVelocityY(0f);
        }
        else if(whenFalling)
        {
            PlayerMovement.SetVelocityY(0f);
        }
        
    }

    static private void SetVelocityX(float x)
    {
        myRigidbody.velocity = new Vector2(x, myRigidbody.velocity.y);
    }

    static private void SetVelocityY(float y)
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, y);
    }

    public void FlipPlayer(float direction)
    {
        if(!PlayerState.GetIsFacingRight() && direction > 0f)
        {
            Flip();
        }
        else if(PlayerState.GetIsFacingRight() && direction < 0f)
        {
            Flip();
        }
    }

    static public void AddVelocity(Vector2 velocity)
    {
        myRigidbody.velocity += velocity;
    }

    static public Vector2 GetVelocity()
    {
        return myRigidbody.velocity;
    }

    public void Flip()
    {
        Vector3 theScale = spriteRenderer.transform.localScale;

        PlayerState.SetIsFacingRight(!PlayerState.GetIsFacingRight());

        theScale.x *= -1;
        
        spriteRenderer.transform.localScale = theScale;
    }

    private IEnumerator JumpTimerBehaviour()
    {
        yield return new WaitForSeconds(jumpTime);

        StopJumping();
    }

    private IEnumerator SlideBehaviour()
    {
        PlayerInputs.GetInputActions().Disable();

        PlayerMovement.StopMoving();

        AddVelocity(Vector2.right * PlayerMovement.facing * slideSpeed);

        yield return new WaitForSeconds(slideTime);

        PlayerInputs.GetInputActions().Enable();

        PlayerState.SetState(PlayerState.State.Idle);

        controlsOverlay.SlideActionCreated(); 
    }
}
