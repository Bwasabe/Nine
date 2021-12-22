using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMove : MonoBehaviour
{
    ///TODOLIST
    //로프엑션 구현하기
    //InputManager 구현하기{
        //https://github.com/jschiff/unity-extensions/blob/9c59f5d370d26f08b22da7b489af65b6a9976e31/Runtime/TextAnimator/Content/Input.cs#L55
    //}
    //캐릭터 공격 구현하기
    //적 캐릭터 베이스 구현하기

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

    public Action getMove{
        get{
            return Move;
        }
    }
    
    public Action getJump{
        get{
            return Jump;
        }
    }
    #endregion


    #region SerializeField

    [SerializeField]
    private Character playerstatus;


    [SerializeField]
    private PhysicsMaterial2D playerMaterial;


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

//TODO : private
    public float hori;
    public float bottomDistance;
    public float speed;
    public float jumpPower;
    public float gravity;


    public int jumpCount;
    public int jumpMaxCount;


    private bool isChangeDirection;
    private bool isBack;

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
        testText.text = string.Format("{0}", speed);
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
        bottomChk.position = new Vector2(col.bounds.min.x, col.bounds.min.y - 0.05f);
        bottomDistance = col.bounds.size.x - 0.1f;
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
    #endregion



    #region Public
    public void SetStatus(Character character)
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
        SetPlayerDirection();
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed * hori, Time.deltaTime * moveSmooth), rb.velocity.y);
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
    }
    private bool IsDownBlock()
    {
        return Physics2D.Raycast(bottomChk.position, ((isBack) ? Vector2.left : Vector2.right), bottomDistance, downLayer);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
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
                if (jumpCount >= jumpMaxCount) return;
                state &= ~PlayerState.JUMPING_DOWN;
                state |= PlayerState.JUMP;
                jumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                return;
            }
        }

        if (IsGround())
        {
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

