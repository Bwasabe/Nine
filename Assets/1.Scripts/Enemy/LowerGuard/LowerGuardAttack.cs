using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMove))]

public class LowerGuardAttack : EnemyAttack
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shootPos;
    [SerializeField]
    private Sprite shootSprite;


    private EnemyMove enemyMove;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    private bool isFire;

    private void Start()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody2D>();
        enemyMove = GetComponent<EnemyMove>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AddFSM();
    }
    protected override void AddFSM()
    {
        base.AddFSM();
        enemyAI.AddFSMAction(FSMStates.FixedUpdate, EnemyAI.States.Attack, ChangeFacing);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, Shooting);

        enemyAI.AddFSMAction(FSMStates.Exit, EnemyAI.States.Attack, ExitAttack);

    }
    protected override void Attack()
    {
        rb.velocity = Vector2.zero;
    }

    private void Shooting()
    {

        if (spriteRenderer.sprite == shootSprite && !isFire)
        {
            Fire();
            isFire = true;
        }
        else if(spriteRenderer.sprite != shootSprite){
            isFire = false;
        }

    }

    private void ChangeFacing()
    {
        if (GameManager.Instance.Player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
        }
        enemyMove.SetIsFacingToLocalScale();
    }
    private void Fire()
    {
        //TODO: 풀링 소환
        Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
        GameObject g = Instantiate(bullet, shootPos.position, Quaternion.identity);
        g.SetActive(true);
        g.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(/*transform.localScale.x > 0 ? transform.right : transform.right * -1f*/transform.right, dir));
    }
    private void ExitAttack()
    {
        //timer = 0f;
    }



}
