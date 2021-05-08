using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    #region  Attributes
    private Animator animator;
    #endregion

    #region  MonoBehaviour Methods
    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }
    #endregion

    #region  Normal Methods
    public void SetTrigger(string name)
    {
        ResetAllTriggers();

        animator.SetTrigger(name);
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Moving");

        animator.ResetTrigger("Idle");

        animator.ResetTrigger("Jumping");
    }

    public void ResetTrigger(string name)
    {
        animator.ResetTrigger(name);
    }
    #endregion
}

