using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Vector2 moveInput;
    private bool isGrounded;
    private float jumpVel;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        moveInput.x = PlayerMaster.Instance.REF_PlayerController.MoveInput.x;
        isGrounded = PlayerMaster.Instance.REF_PlayerController.IsGrounded;
        jumpVel = PlayerMaster.Instance.REF_PlayerController.JumpVelocity;

        if (moveInput.x < -0.1f || moveInput.x > 0.1f)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("jumpVel", jumpVel);
    }
}
