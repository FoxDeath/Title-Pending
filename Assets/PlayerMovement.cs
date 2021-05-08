using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] float gravity;

    private Coroutine jumpTimerCoroutine;

    private static Rigidbody2D myRigidbody;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Gravity()
    {
        myRigidbody.velocity += Vector2.down * gravity;
    }

    public void Movement()
    {
        if(PlayerState.GetState() != PlayerState.State.Sliding)
        {
            FlipPlayer();
        }

        myRigidbody.velocity += new Vector2(PlayerInputs.GetMoveInput() * speed, 0f);
    }

    public void Jump()
    {
        if(jumpTimerCoroutine != null)
        {
            StopCoroutine(jumpTimerCoroutine);
        }

        myRigidbody.velocity += Vector2.up * jumpForce;

        jumpTimerCoroutine = StartCoroutine(JumpTimerBehaviour());
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
        if(PlayerInputs.GetMoveInput() == 0f)
        {
            PlayerMovement.SetVelocityX(0f);
        }
        else if(noInput)
        {
            PlayerMovement.SetVelocityX(0f);
        }
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

    public void FlipPlayer()
    {
        if(!PlayerState.GetIsFacingRight() && PlayerInputs.GetMoveInput() > 0f)
        {
            Flip();
        }
        else if(PlayerState.GetIsFacingRight() && PlayerInputs.GetMoveInput() < 0f)
        {
            Flip();
        }
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
}
