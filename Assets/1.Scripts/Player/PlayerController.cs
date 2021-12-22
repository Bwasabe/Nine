using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Action
    public event Action attack;
    public event Action slide;
    public event Action colEnter;
    public event Action colExit;
    #endregion

    [SerializeField]
    private Character player;


    private PlayerMove playerMove;

    private Rigidbody2D rb;


    private float slidingSpeed;
    private float slidingDuration;


    private bool isSlide;

    #region 이벤트
    private void Awake()
    {
        attack += () => { };
        slide += () => { };
        colEnter += () => { };
        colExit += () => { };
    }
    private void OnEnable() {
        InitAction();
    }
    private void Start()
    {
        playerMove = GameManager.Instance.PlayerMove;
        rb = GetComponent<Rigidbody2D>();
        SetStatus(player);
    }
    private void Update()
    {
        slide();
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
    }
    private void Sliding()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide)
        {
            isSlide = true;
            Debug.Log(slidingDuration);
            StartCoroutine(Slide());
        }
    }




    private IEnumerator Slide()
    {
        playerMove.jump -= playerMove.getJump;
        playerMove.move -= playerMove.getMove;                          // -180
        rb.velocity = new Vector2(slidingSpeed * ((transform.rotation.y == 0) ? 1 : -1), rb.velocity.y);
        yield return Yields.WaitSeconds(slidingDuration);
        playerMove.move += playerMove.getMove;
        playerMove.jump += playerMove.getJump;

        isSlide = false;
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
