using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    #endregion


    #region SerializeField
    [SerializeField]
    private Character playerstatus;


    [SerializeField]
    private Transform bottomChk;
    [SerializeField]
    private LayerMask bottomLayer;
    [SerializeField]
    private LayerMask downLayer;

    [SerializeField]
    private float linearDrag;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpSmooth;

    #endregion

    private PlayerState state;

    private Character currentStatus;


    private Rigidbody2D rb;
    private BoxCollider2D col;


    private float hori;
    private float bottomDistance;
    private float jumpTimer;

    private bool isChangeDirection;
    private bool isBack;

    #region 이벤트
    private void Awake()
    {
        move += () => { };
        jump += () => { };
    }
    private void Start()
    {
        Initialize();
        InitValue();
    }
    private void Update()
    {
        move();
        jump();
    }
    #endregion

    #region Initialize
    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    private void InitValue()
    {
        bottomChk.position = new Vector2(col.bounds.min.x, col.bounds.min.y - 0.1f);
        bottomDistance = col.bounds.size.x;
        SetStatus(playerstatus);
        move += () => Move();
        jump += () => Jump();
    }
    #endregion



    #region Public
    public void SetStatus(Character character)
    {
        currentStatus = character;
    }
    #endregion


    private void Move()
    {
        hori = Input.GetAxisRaw("Horizontal");
        isChangeDirection = (hori > 0f && rb.velocity.x < 0f) || (hori < 0f && rb.velocity.x > 0f);

        if (IsGround())
        {
            if (hori == 0 || isChangeDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
        }
        Debug.Log(rb);
        rb.velocity = new Vector2(currentStatus.speed * hori, rb.velocity.y);
    }
    private void SetPlayerDirection()
    {
        if (hori == 0)
        {
            // 대충 애니메이션
        }
        else
        {
            isBack = hori < 0;
            transform.rotation = Quaternion.Euler(0f, (isBack) ? 180f : 0f, 0f);
        }
    }

    private bool IsGround()
    {
        Debug.DrawRay(bottomChk.position, ((isBack) ? Vector2.left : Vector2.right) * bottomDistance, Color.blue);
        return Physics2D.Raycast(bottomChk.position, ((isBack) ? Vector2.left : Vector2.right), bottomDistance, bottomLayer);
        //Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Downable"));
    }
    private bool IsDownBlock()
    {
        return Physics2D.Raycast(bottomChk.position, ((isBack) ? Vector2.left : Vector2.right), bottomDistance, downLayer);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGround())
            {
                if (Input.GetAxisRaw("Vertical") < 0f && IsDownBlock())
                {
                    if (state.HasFlag(PlayerState.JUMPING_DOWN)) return;
                    state |= PlayerState.JUMPING_DOWN;
                    StartCoroutine(JumpingDown());
                    return;
                }
                else
                {
                    state |= PlayerState.JUMP;
                }
            }
            if (currentStatus.jumpCount >= currentStatus.jumpMaxCount) return;
            currentStatus.jumpCount++;
        }
        if (state.HasFlag(PlayerState.JUMP))
        {
            if (Input.GetButton("Jump") && jumpTimer < currentStatus.jumpMaxTime)
            {
                jumpTimer += Time.deltaTime;
                Debug.Log(rb.velocity.y);
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y , currentStatus.jumpPower , Time.deltaTime * jumpSmooth));
            }
            else if (((jumpTimer >= currentStatus.jumpMaxTime) || !Input.GetButton("Jump")))
            {
                state &= ~PlayerState.JUMP;
                jumpTimer = 0f;
            }
        }
        if (IsGround())
        {
            currentStatus.jumpCount = 0;
        }
        else
        {
            rb.gravityScale = currentStatus.gravity;
            rb.drag = linearDrag * 0.5f;
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

