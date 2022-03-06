using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private Player player;

    private PlayerState state = PlayerState.NONE;

    private PlayerMove playerMove;

    private PlayerAttack playerAttack;

    private Rigidbody2D rb;
    private Animator animator;
    private AnimatorClipInfo[] animatorClipInfos;

    private float slidingSpeed;
    private float slidingDuration;

    private int attackCount;

    [SerializeField]//TODO: Delete
    private int cardCount;

    private bool attactAgane = false;
    [SerializeField]
    private ParticleSystem myParticleSystem;
    [SerializeField]
    private hi ghostMode;
    private SpriteRenderer spriteRenderer;

    #region 이벤트
    private void Awake()
    {
        attack += () => { };
        slide += () => { };
        colEnter += () => { };
        colExit += () => { };
        ghostMode = GetComponentInChildren<hi>();
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
        playerAttack = GetComponent<PlayerAttack>();
        SetStatus(player);
        animatorClipInfos = animator.GetCurrentAnimatorClipInfo(0);
    }
    private void Update()
    {
        slide();
        attack();
        if(state.HasFlag(PlayerState.ATTACK)){
            //playerMove.AttackWalk();
            if(animator.GetBool("IsGround") == false){
                if(animator.GetFloat("AttackCount") == 0){
                    animator.SetFloat("AttackCount", 3);
                }
            }
            else if(animator.GetBool("IsGround") == true){
                if(animator.GetFloat("AttackCount") == 3){
                    animator.SetFloat("AttackCount", 0);
                }else if(animator.GetFloat("AttackCount") == 4){
                    animator.SetFloat("AttackCount", 2);
                }
            }
        }
    }
    #endregion

    #region Public
    public void SetStatus(Player character)
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
        if (Input.GetKeyDown(InputManager.keyMaps[Keys.SLIDE]) && !state.HasFlag(PlayerState.SLIDE))
        {
            state |= PlayerState.SLIDE;
            StartCoroutine(Slide());
            

        }
    }


    private IEnumerator Slide()
    {
        playerMove.IsFreeze();
        //rb.drag = 0f;
        //rb.gravityScale = 0f;
        //playerMove.useGravity = false;
        //rb.velocity = new Vector2(slidingSpeed * (((playerMove.hori != 0) ? (playerMove.hori > 0) : !playerMove.isBack) ? 1 : -1), 0f);
        ghostMode.GOGhost(slidingDuration, 0.05f);
        rb.velocity = new Vector2(slidingSpeed * (!playerMove.isBack ? 1 : -1), 0f);
        animator.ResetTrigger("SlideEnd");
        animator.Play("PlayerSlide");
        playerMove.SlideTween = DOTween.To(()=> rb.velocity, x=> rb.velocity = x, new Vector2((!playerMove.isBack ? 1 : -1)*7f,0), slidingDuration).SetEase(Ease.OutQuad);
        yield return Yields.WaitForSeconds(slidingDuration);
        animator.SetTrigger("SlideEnd");
        //playerMove.useGravity = true;
        //rb.gravityScale = 3.2f;
        //rb.drag = 3.7f;
        state &= ~PlayerState.SLIDE;
        if(!state.HasFlag(PlayerState.ATTACK)){
            playerMove.IsMove();
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(InputManager.keyMaps[Keys.ATTACK]))
        {
            attactAgane = true;
            if(!state.HasFlag(PlayerState.ATTACK)){
                state |= PlayerState.ATTACK;
                StartCoroutine(Attacking());
            }
        }
        
        
    }
    private IEnumerator Attacking()
    {
        bool isGround = playerMove.IsGround();

        playerMove.IsFreeze();
        animator.Play("PlayerAttack", -1, 0f);
        cardCount++;
        attackCount = (isGround) ? (attackCount == 0) ? 1 : 0 : 3;
        
        playerAttack.Attack(cardCount);
        if(cardCount>=6){
            animator.SetFloat("AttackCount", (isGround) ? 2 : 4);
            
            cardCount = 0;
        }else{
            animator.SetFloat("AttackCount", attackCount);
        }

        attactAgane = false;

        if(!isGround){
            yield return Yields.WaitForSeconds(0.1f);
        }
        yield return Yields.WaitForSeconds((cardCount==0)?0.45f:0.35f);
        
        
        playerAttack.OffCol();
        playerMove.ChackHori();
        playerMove.SetPlayerDirection();
        if(attactAgane){
            StartCoroutine(Attacking());
        }else{
            playerMove.IsMove();
            state &= ~PlayerState.ATTACK;
        }
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
