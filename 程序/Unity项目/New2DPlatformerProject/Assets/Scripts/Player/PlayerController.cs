using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //玩家输入的移动方向
    private float movementInputDirection;
    private Rigidbody2D rb;
    public float movementSpeed = 10;

    // =========================跳跃相关=========================
    public float jumpForce = 16.0f; //跳跃力
    public float movementForceInAir; //空中移动
    //地面监测
    public Transform groundCheck;
    private bool isGrounded;
    public float groundCheckRadius; //检测圆半径
    public LayerMask whatIsGround;
    private bool canJump;

    //2级跳
    public int amountOfJumps = 1;
    private int amountOfJumpLeft; //可以跳跃的剩余次数
    public float airDragMultiplier = 0.95f; //跳跃落地时候的阻力

    //大小跳
    public float variableJumpHeightMultiplier = 0.5f;


    // ==========================爬墙
    private bool isTouchingWall;
    public float wallCheckDistance;
    public Transform wallCheck;
    private bool isWallSliding;
    public float wallSlidingSpeed;

    //爬墙跳
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public float wallHopForce;
    public float wallJumpForce;
    public int facingDirection = 1;



    //动画相关
    private Animator anim;
    private bool isWalking;


    private bool isFacingRight = true; //默认是面向右边
    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();


    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSlide();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();

    }
    private void CheckInput()
    {   
        //按A则为-1， D为1 ，如果没有Raw，则为-1~0 和 0~1的值，按得越长则数值越高
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }
    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void CheckSurroundings()
    {
        //检测墙面或地面
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //射线从哪里射向哪里,方向是什么
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }
    private void CheckIfWallSlide()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else{
            isWallSliding = false;
        }
    }
    private void CheckIfCanJump()
    {
        if((isGrounded && rb.velocity.y <= 0)||isWallSliding)
        {
            amountOfJumpLeft = amountOfJumps;
        }
        if(amountOfJumpLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void Jump()
    {
        if (canJump && !isWallSliding && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpLeft--;
        }
        /*else if (isWallSliding && movementInputDirection == 0 && canJump) // wall hop
        {
            isWallSliding = false;
            amountOfJumpLeft--;
            Vector2 forceAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceAdd, ForceMode2D.Impulse);
        }*/
        /* 爬墙的时候，需要判定是否按了方向键
         * else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpLeft--;
            Vector2 forceAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceAdd, ForceMode2D.Impulse);
        }*/
        else if ((isWallSliding || isTouchingWall) && canJump)
        {
            isWallSliding = false;
            amountOfJumpLeft--;
            Vector2 forceAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceAdd, ForceMode2D.Impulse);
        }
    }


    private void ApplyMovement()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
        //如果滑墙的时候不可以按左右取消，则加上 !isSliding
        else if (!isGrounded && movementInputDirection != 0)
        {   
            //跳起来的时候的水平力
            Vector2 forceAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed*movementInputDirection,rb.velocity.y);
            }
        }
        else if(!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x* airDragMultiplier, rb.velocity.y);
        }

        
        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight; //直接互换
            transform.Rotate(0.0f, 180.0f, 0.0f);   
        }
    }

    //画圆，用于调整
    private void OnDrawGizmos()
    {   
        //为了调整地面监测人物的中心位置
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z)); 
    }

}
