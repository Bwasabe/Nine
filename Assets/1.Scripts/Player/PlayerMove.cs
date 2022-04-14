using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerMove : MonoBehaviour
{


    [Flags]
    private enum PlayerState
    {
        NONE = 0,
        MOVE = 1 << 1,
        JUMP = 1 << 2,
        JUMPING_DOWN = 1 << 3,
        DOWNABLE = 1 << 4,
    }
    #region Action


    private PlayerController playerController;
    public Action getMove
    {
        get
        {
            return Move;
        }
    }

    public Action getJump
    {
        get
        {
            return Jump;
        }
    }
    #endregion


    #region SerializeField

    [SerializeField]
    private Player playerstatus;



    [SerializeField]
    private Text testText;

    [SerializeField]
    private Transform bottomChk, wallChk;
    [SerializeField]
    private LayerMask bottomLayer;
    [SerializeField]
    private LayerMask downLayer;

    [SerializeField]
    private float linearDrag;
    [SerializeField]
    private float jumpDrag;
    [SerializeField]
    private float moveSmooth;
    [SerializeField]
    private float jumpSmooth;

    #endregion

    private PlayerState state;


    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    [SerializeField]
    public SpriteRenderer spriteRenderer;

    //TODO : private
    public float hori;
    public float bottomDistance;
    public float wallDistance;
    public float speed;
    public float jumpPower;
    public float gravity;


    public int jumpCount;
    public int jumpMaxCount;

    public bool isBack { get; private set; }
    
    private bool isChangeDirection;
    public bool useGravity = true;
    public Tween SlideTween = null;

    private bool canMove;
    private bool canJump;

    #region 이벤트
    private void OnEnable()
    {
        IsMove();
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        Initialize();
        InitValue();
    }

    private void Update()
    {
        //testText.text = string.Format("{0}", (int)rb.velocity.y);
        Debug.DrawRay(bottomChk.position, Vector2.right * bottomDistance, Color.red);
        Debug.DrawRay(wallChk.position, Vector2.right  * (isBack?-1:1)* wallDistance, Color.red);
        Move();
        Jump();
        Ground();
        SetPlayerDirection();
    }
    #endregion

    #region Initialize
    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void InitValue()
    {
        bottomChk.position = new Vector2(col.bounds.min.x, col.bounds.min.y - 0.1f);
        wallChk.position = new Vector2((col.bounds.min.x+col.bounds.max.x)/2f, (col.bounds.min.y+col.bounds.max.y)/2f);

        bottomDistance = col.bounds.size.x;
        wallDistance = col.bounds.size.x+0.4f;
        SetStatus(playerstatus);
    }


    public void IsFreeze()
    {
        canMove = false;
        hori = 0;
        AttackWalk();
    }
    public void AttackWalk(){
        if(IsGround()){
            rb.velocity = new Vector2((isBack?-1f:1f)*2f, rb.velocity.y);
        }
    }
    public void Stop(){
        rb.velocity = new Vector2((isBack?-1f:1f)*4f, rb.velocity.y);
    }
    public void IsMove()
    {
        canMove = canJump = true;
    }
    #endregion



    #region Public
    public void SetStatus(Player character)
    {
        speed = character.speed;
        jumpPower = character.jumpPower;
        jumpCount = character.jumpCount;
        jumpMaxCount = character.jumpMaxCount;
        gravity = character.gravity;
    }
    #endregion

    public void Move()
    {
        if(!canMove)return;
        ChackHori();
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if (IsGround())
        {
            isChangeDirection = (hori > 0f && rb.velocity.x < 0f) || (hori < 0f && rb.velocity.x > 0f);
            if (hori == 0 || isChangeDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
        }
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed * hori, Time.deltaTime * moveSmooth), rb.velocity.y);
    }
    public void ChackHori(){
        if (Input.GetKey(InputManager.keyMaps[Keys.LEFT]) && Input.GetKey(InputManager.keyMaps[Keys.RIGHT]))
        {
            hori = 0;
        }
        else if (Input.GetKey(InputManager.keyMaps[Keys.LEFT]))
        {
            hori = -1;
        }
        else if (Input.GetKey(InputManager.keyMaps[Keys.RIGHT]))
        {
            hori = 1;
        }
        else
        {
            hori = 0;
        }
    }
    public void SetPlayerDirection()
    {
        if (hori == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
            isBack = hori < 0;
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, (isBack) ? 180f : 0f, 0f);
            //transform.localScale = new Vector3(isBack ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
            //transform.rotation = Quaternion.Euler(0f, (hori < 0) ? 180f : 0f, 0f);
        }
    }

    public bool IsGround()
    {
        return Physics2D.Raycast(bottomChk.position, Vector2.right, bottomDistance, bottomLayer);
    }
    public bool IsWall()
    {
        return Physics2D.Raycast(wallChk.position, Vector2.right * (isBack?-1:1), wallDistance, bottomLayer);
    }
    private bool IsDownBlock()
    {
        return Physics2D.Raycast(bottomChk.position, Vector2.right, bottomDistance, downLayer);
    }

    private void Jump()
    {
        if(!canJump)return;
        if (Input.GetKeyDown(InputManager.keyMaps[Keys.JUMP]))
        {
            if (Input.GetKey(InputManager.keyMaps[Keys.DOWN]) && IsDownBlock())
            {
                if (state.HasFlag(PlayerState.JUMPING_DOWN)) return;
                state |= PlayerState.JUMPING_DOWN;
                StartCoroutine(JumpingDown());
                return;
            }
            else
            {
                if (jumpCount >= jumpMaxCount) return;
                IsMove();
                SlideTween.Kill();
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
                animator.SetTrigger("Jump");
                animator.Play("PlayerJump");
                state &= ~PlayerState.JUMPING_DOWN;
                state |= PlayerState.JUMP;
                jumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                useGravity = true;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                return;
            }
        }
        animator.SetFloat("VelocityY", rb.velocity.y);


    }
    public bool IsJumping(){
        return state.HasFlag(PlayerState.JUMP);
    }
    private void Ground()
    {
        if (useGravity && IsGround())
        {
            animator.SetBool("IsGround", true);
            if (state.HasFlag(PlayerState.JUMP))
            {
                state &= ~PlayerState.JUMP;
            }
            else if (rb.velocity.y <= 0.1f)
            {
                if(!playerController.IsSliding()){
                    jumpCount = 0;
                }
            }
        }
        else
        {
            if (jumpCount == 0)
            {
                jumpCount = 1;
            }
            animator.SetBool("IsGround", false);
            rb.gravityScale = gravity;
            rb.drag = linearDrag * jumpDrag;
        }
    }

    private IEnumerator JumpingDown()
    {
        col.isTrigger = true;
        rb.AddForce(Vector2.down, ForceMode2D.Impulse);

        // 바닥과 충돌하였는지 체크
        while (IsGround())
        {
            yield return null;
        }

        yield return new WaitUntil(() => IsGround());
        col.isTrigger = false;
        state = PlayerState.NONE;
    }


}

