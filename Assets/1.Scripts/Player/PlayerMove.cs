using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bottomchkDistance;
    [SerializeField]
    private float jumpMaxTime, jumpingTime, jumpPower;
    [SerializeField]
    private float slidingSpeed, slidingTime, slidingMaxTime;

    [SerializeField]
    private Transform bottomChk;
    [SerializeField]
    private LayerMask b_layerMask;

    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D col = null;

    public bool isGround, isJumping, isSlide;
    public bool isBack = false; // private

    private float horizonXM, hori, Xrate;

    public int jumpCount = 0, maxjumpCount = 1; //private

    private void Start()
    {
        if (!myRigidbody2D) myRigidbody2D = GetComponent<Rigidbody2D>();

        if (!col) col = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        Move();
        Jump();
        BottomChk();
        Sliding();
    }
    private void Jump()
    {
        if (isSlide) return;
        if (isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxjumpCount)
        {
            jumpCount++;
            isJumping = true;
            jumpingTime = 0f;
        }
        if (isJumping)
            jumpingTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && isJumping && jumpingTime < jumpMaxTime)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpPower - jumpingTime * 4f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpingTime = 0f;
        }
    }
    void BottomChk()
    {
        Debug.DrawRay(bottomChk.position, ((isBack) ? Vector3.left : Vector3.right) * bottomchkDistance, Color.blue);
        isGround = Physics2D.Raycast(bottomChk.position, ((isBack) ? Vector3.left : Vector3.right), bottomchkDistance, b_layerMask);
        if (isGround)
        {
            if (!isJumping)
            {
                jumpCount = 0;
            }
        }
    }
    private void Move()
    {
        if (isSlide) return;
        hori = Input.GetAxisRaw("Horizontal");
        if (hori == 0)
        {
            Xrate = 8f;
        }
        else
        {
            Xrate = 3f;
            isBack = (hori < 0);
            transform.rotation = Quaternion.Euler(0f, (isBack) ? 180f : 0f, 0f);
        }
        horizonXM = Mathf.Lerp(horizonXM, hori * speed, Time.deltaTime * Xrate);
        myRigidbody2D.velocity = new Vector2(horizonXM, myRigidbody2D.velocity.y);
    }
    private void Sliding()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide)
        {
            isSlide = true;
            myRigidbody2D.velocity = new Vector2(slidingSpeed * ((isBack) ? -1 : 1), myRigidbody2D.velocity.y);
        }
        if (isSlide)
        {
            slidingTime += Time.deltaTime;
        }
        if (slidingTime > slidingMaxTime)
        {
            isSlide = false;
            slidingTime = 0;
        }
    }
    public void MinusJumpCount(int value){
        jumpCount -= value;
    }
}