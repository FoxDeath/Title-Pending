using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsCalculations : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    private PlayerInputs playerInputs;

    private CapsuleCollider2D playerCollider;

    static public RaycastHit2D hitV;
    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();

        playerInputs = GetComponent<PlayerInputs>();
    }

    private void FixedUpdate()
    {
        RayCastCalculation(out hitV);

        ColiderCalculation(hitV);
    }

    private void RayCastCalculation(out RaycastHit2D hitV)
    {
        hitV = Physics2D.BoxCast(playerCollider.bounds.min, new Vector2(3.5f, 1f), 0f, Vector2.down, playerCollider.bounds.size.y - 4.8f * 1f, groundLayer);
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

            PlayerState.SetState(PlayerState.State.Falling);
        }
    }
}
