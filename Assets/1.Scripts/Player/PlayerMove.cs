using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    public event Action move;
    public event Action jump;

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
    private Transform bottomChk;
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
    private SpriteRenderer spriteRenderer;

    //TODO : private
    public float hori;
    public float bottomDistance;
    public float speed;
    public float jumpPower;
    public float gravity;


    public int jumpCount;
    public int jumpMaxCount;

    public bool isBack{get; private set;}
    private bool isChangeDirection;

    #region 이벤트
    private void OnEnable()
    {
        DefaultAction();
        InitAction();
    }
    private void Start()
    {
        Initialize();
        InitValue();
    }

    private void Update()
    {
        //testText.text = string.Format("{0}", (int)rb.velocity.y);
        Debug.DrawRay(bottomChk.position, Vector2.right * bottomDistance, Color.red);
        move();
        jump();
    }
    #endregion

    #region Initialize
    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void InitValue()
    {
        bottomChk.position = new Vector2(col.bounds.min.x, col.bounds.min.y - 0.05f);
        bottomDistance = col.bounds.size.x;
        SetStatus(playerstatus);
    }
    private void InitAction()
    {
        move += Move;
        jump += Jump;
    }
    public void DefaultAction()
    {
        move = () => { };
        jump = () => { };
    }
    public void IsFreeze()
    {
        move -= Move;
        jump -= Jump;
    }
    public void IsMove()
    {
        move += Move;
        jump += Jump;
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

    private void Move()
    {
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
        SetPlayerDirection();
    }
    private void SetPlayerDirection()
    {
        if (hori == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
            isBack = hori < 0;
            spriteRenderer.flipX = isBack;
            //transform.localScale = new Vector3(isBack ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
            //transform.rotation = Quaternion.Euler(0f, (hori < 0) ? 180f : 0f, 0f);
        }
    }

    public bool IsGround()
    {
        return Physics2D.Raycast(bottomChk.position, Vector2.right, bottomDistance, bottomLayer);
    }
    private bool IsDownBlock()
    {
        return Physics2D.Raycast(bottomChk.position, Vector2.right, bottomDistance, downLayer);
    }

    private void Jump()
    {
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
                animator.Play("PlayerJump");
                state &= ~PlayerState.JUMPING_DOWN;
                state |= PlayerState.JUMP;
                jumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                return;
            }
        }
        animator.SetFloat("VelocityY", rb.velocity.y);

        if (IsGround())
        {
            animator.SetBool("IsGround", true);
            if (state.HasFlag(PlayerState.JUMP))
            {
                state &= ~PlayerState.JUMP;
            }
            else if (rb.velocity.y <= 0.1f)
            {
                jumpCount = 0;

            }
        }
        else
        {
            if(jumpCount == 0){
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

