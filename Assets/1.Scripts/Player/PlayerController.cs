using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Flags]
    private enum PlayerState
    {
        NONE = 0,
        SLIDE = 1 << 1,
        ATTACK = 1 << 2,

    }

    [SerializeField]
    private Text testText;

    #region Action

    public event Action attack;
    public event Action slide;
    public event Action colEnter;
    public event Action colExit;
    public event Action interact;


    public Action getSlide
    {
        get
        {
            return Sliding;
        }
    }
    #endregion

    [SerializeField]
    private Character player;

    private PlayerState state = PlayerState.NONE;

    private PlayerMove playerMove;

    private Rigidbody2D rb;
    private Animator animator;
    private AnimatorClipInfo[] animatorClipInfos;

    private float slidingSpeed;
    private float slidingDuration;

    private int attackCount;


    #region 이벤트
    private void Awake()
    {
        attack += () => { };
        slide += () => { };
        colEnter += () => { };
        colExit += () => { };
    }
    private void OnEnable()
    {
        InitAction();
    }
    private void Start()
    {
        playerMove = GameManager.Instance.PlayerMove;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        SetStatus(player);
        animatorClipInfos = animator.GetCurrentAnimatorClipInfo(0);
    }
    private void Update()
    {
        testText.text = string.Format("{0}", animatorClipInfos[0].clip.length);
        slide();
        attack();
    }
    #endregion

    #region Public
    public void SetStatus(Character character)
    {
        slidingSpeed = character.slidingSpeed;
        slidingDuration = character.slidingDuration;
    }
    #endregion

    private void InitAction()
    {
        slide += Sliding;
        attack += Attack;
    }
    private void Sliding()
    {
        if (Input.GetKeyDown(KeySetting.keyMaps[Keys.SLIDE]) && !state.HasFlag(PlayerState.SLIDE) && playerMove.IsGround())
        {
            state |= PlayerState.SLIDE;
            StartCoroutine(Slide());
        }
    }


    private IEnumerator Slide()
    {
        playerMove.IsFreeze();
        rb.drag = 0f;
        rb.velocity = new Vector2(slidingSpeed * ((transform.rotation.y == 0) ? 1 : -1), rb.velocity.y);
        yield return Yields.WaitForSeconds(slidingDuration);
        rb.drag = 3.7f;
        playerMove.IsMove();
        state &= ~PlayerState.SLIDE;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeySetting.keyMaps[Keys.ATTACK]) && !state.HasFlag(PlayerState.ATTACK))
        {
            state |= PlayerState.ATTACK;
            StartCoroutine(Attacking());
        }
    }
    private IEnumerator Attacking()
    {
        animator.Play("PlayerAttack");
        animator.SetFloat("AttackCount", attackCount);
        yield return Yields.WaitForSeconds(0.4f);
        attackCount = (attackCount == 0) ? 1 : 0;

        state &= ~PlayerState.ATTACK;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colEnter();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        colExit();
    }



}
