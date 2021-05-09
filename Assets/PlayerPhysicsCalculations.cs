using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsCalculations : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    private PlayerInputs playerInputs;

    private CapsuleCollider2D playerCollider;

    static public RaycastHit2D hitV, hitSlide;
    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();

        playerInputs = GetComponent<PlayerInputs>();
    }

    private void FixedUpdate()
    {
        RayCastCalculation(out hitV, out hitSlide);

        ColiderCalculation(hitV);
    }

    private void RayCastCalculation(out RaycastHit2D hitV, out RaycastHit2D hitSlide)
    {
        hitV = Physics2D.BoxCast(playerCollider.bounds.min, new Vector2(4f, 1f), 0f, Vector2.down, playerCollider.bounds.size.y - 4.8f * 1f, groundLayer);
    
        hitSlide = Physics2D.Raycast(transform.position + new Vector3(0f, -0.4f), new Vector2(0f, 1f), 3.3f, groundLayer);
    }

    private void ColiderCalculation(RaycastHit2D hitV)
    {
        if(hitV.collider)
        {
            PlayerState.SetIsGrounded(true);
        }
        else
        {
            PlayerState.SetIsGrounded(false);

            if(PlayerState.GetState() != PlayerState.State.Sliding)
            {
                PlayerState.SetState(PlayerState.State.Falling);
            }
        }
    }
}
