using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The PlayerController manages different valuebles like movement-speed, accselaration and more.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerMaster _master;

    public Vector2 MoveInput { get { return moveInput; } private set { MoveInput = value; } }
    public float JumpVelocity { get { return rbody.velocity.y; } private set { JumpVelocity = value; } }
    public bool IsGrounded { get { return isGrounded; } private set { IsGrounded = value; } }
    public bool IsWalking { get { return _isWalking; } private set { IsWalking = value; } }
    public bool IsRunning { get { return _isRunning; } private set { IsRunning = value; } }
    public bool IsCrouching { get { return _isCrouching; } private set { IsCrouching = value; } }
    public bool IsCrawling { get { return _isCrawling; } private set { IsCrawling = value; } }





    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private bool canJump = true;


    #region Movement
    //serialized
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float deceleration;
    [SerializeField]
    private float airAccel;
    [SerializeField]
    private float airDecel;
    [SerializeField]
    private float velPower;
    [SerializeField]
    private float friction;

    //privates
    private float moveSpeed;
    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private bool _isWalking;
    private bool _isRunning;
    private bool _isCrouching;
    private bool _isCrawling;
    #endregion


    #region Jump
    //serialized
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCutMultiplier;
    [SerializeField]
    private float fallGravityMultiplier;
    [SerializeField]
    private float jumpCoyoteTime;
    [SerializeField]
    private float jumpBufferTime;

    //privates
    private float gravityScale;
    private bool isJumping;
    private bool jumpInputReleased;
    #endregion


    #region Checks
    //serialized
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private Vector2 groundCheckSize;

    //privates
    private float lastGroundedTime;
    private float lastJumpTime;
    private bool isFacingRight = true;
    private bool isGrounded;
    #endregion


    #region Layers & Tags
    //serialized
    [SerializeField]
    private LayerMask groundLayer;
    #endregion


    #region Debugging
    //serialized
    [SerializeField]
    private bool showMoveTrail;
    #endregion


    #region Input
    private bool jumpPressedDown = false;
    #endregion


    private Rigidbody2D rbody;
    private TrailRenderer trailRenderer;


    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
        rbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        gravityScale = rbody.gravityScale;
    }

    #region Input System CallMethods
    public void IA_Move(InputAction.CallbackContext context)
    {
        moveInput = context.action.ReadValue<Vector2>();
        if (context.started)
        {
            _isWalking = true;
        }
        if (context.canceled)
        {
            _isWalking = false;
        }
    }

    public void IA_Jump(InputAction.CallbackContext context)
    {
        if (!canJump) return;
        if (context.started)
        {
            jumpPressedDown = true;
        }
        if (context.canceled)
        {
            jumpPressedDown = false;
            OnJumpUp();
        }
    }

    public void IA_Sprint(InputAction.CallbackContext context)
    {
        if (!isGrounded && MoveInput.x == 0 && _master.REF_PlayerStats.CurrentStamina <= 0)
        {
            _isRunning = false;
            return;
        }
        if (context.started)
        {
            _isCrouching = false;
            _isRunning = true;
        }
        if (context.canceled)
        {
            _isRunning = false;
        }
    }

    public void IA_Crouch(InputAction.CallbackContext context)
    {
        if (!isGrounded)
        {
            _isCrouching = false;
            return;
        }
        if (context.started)
        {
            _isRunning = false;
            _isCrouching = true;
        }
        if (context.canceled)
        {
            _isCrouching = false;
        }
    }
    #endregion

    private void Update()
    {
        #region Inputs
        if (jumpPressedDown)
        {
            lastJumpTime = jumpBufferTime;
        }
        #endregion

        #region Run
        if (moveInput.x != 0)
            lastMoveInput.x = moveInput.x;
        if (moveInput.y != 0)
            lastMoveInput.y = moveInput.y;

        if ((lastMoveInput.x > 0 && !isFacingRight) || (lastMoveInput.x < 0 && isFacingRight))
        {
            Turn();
            isFacingRight = !isFacingRight;
        }
        #endregion

        #region Ground
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer) && !isJumping)
        {
            lastGroundedTime = jumpCoyoteTime;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        #endregion

        #region Jump
        if (rbody.velocity.y <= 0)
        {
            isJumping = false;
        }

        if (lastJumpTime > 0 && !isJumping && jumpInputReleased)
        {
            if (lastGroundedTime > 0)
            {
                lastGroundedTime = 0;
                Jump(jumpForce);
            }
        }
        #endregion

        #region Timer
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        #endregion

        #region Debugging
        trailRenderer.enabled = showMoveTrail;
        #endregion
    }

    private void FixedUpdate()
    {
        #region Run
        if (canMove)
        {
            if (_isRunning && _master.REF_PlayerStats.CurrentStamina > 0)
            {
                moveSpeed = runSpeed;
            }
            else if (_isCrouching)
            {
                moveSpeed = crouchSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            //calculate the direction we want to move in and our desired velocity
            float targetSpeed = moveInput.x * moveSpeed;
            //calculate difference between current velocity and desired velocity
            float speedDif = targetSpeed - rbody.velocity.x;

            //change acceleration rate depending on situation
            float accelRate;
            if (lastGroundedTime > 0)
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            }
            else
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAccel : deceleration * airDecel;
            }

            //applies acceleration to speed difference, the raises to a set power so acceleration increases with higher speeds
            //finally multiplies by sign to reapply direction
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            //applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis 
            rbody.AddForce(movement * Vector2.right);
        }
        #endregion

        #region Friction
        //check if we're grounded and that we are trying to stop (not pressing forwards or backwards)
        if (lastGroundedTime > 0 && !isJumping && Mathf.Abs(moveInput.x) < 0.01f)
        {
            //then we use either the friction amount (~ 0.2) or our velocity
            float amount = Mathf.Min(Mathf.Abs(rbody.velocity.x), Mathf.Abs(friction));
            //sets to movement direction
            amount *= Mathf.Sign(rbody.velocity.x);
            //applies force against movement direction
            rbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Jump Gravity
        if (rbody.velocity.y < 0 && lastGroundedTime <= 0)
        {
            rbody.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rbody.gravityScale = gravityScale;
        }
        #endregion
    }


    #region Jump
    private void Jump(float jumpForce)
    {
        rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastJumpTime = 0;
        isJumping = true;
        jumpInputReleased = false;
    }

    public void OnJump()
    {
        lastJumpTime = jumpBufferTime;
        jumpInputReleased = false;
    }

    public void OnJumpUp()
    {
        if (rbody.velocity.y > 0 && isJumping)
        {
            //reduces current y velocity by amount (0 - 1)
            rbody.AddForce(Vector2.down * rbody.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }

        jumpInputReleased = true;
        lastJumpTime = 0;
    }
    #endregion

    private IEnumerator StopMovement(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (groundCheckPoint != null)
        {
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }
    }
}
