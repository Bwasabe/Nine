using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyAttack : MonoBehaviour
{
    protected EnemyAI enemyAI;
    protected EnemyFOV enemyFOV;
    private EnemyMove enemyMove;

    protected SpriteRenderer spriteRenderer;

    protected bool isFire;


    [SerializeField]
    private Sprite shootSprite;

    [SerializeField]
    private Sprite endAttackSprite;



    private void Start()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyFOV = GetComponent<EnemyFOV>();
        enemyMove = GetComponent<EnemyMove>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AddFSM();
    }
    protected virtual void AddFSM()
    {
        //enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, ReturnToChaseAttack);
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Attack, AttackEnter);
        enemyAI.AddFSMAction(FSMStates.FixedUpdate, EnemyAI.States.Attack, ChangeFacing);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, Attacking);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, CheckBungOff);
    }
    private void Attacking()
    {

        if (spriteRenderer.sprite == shootSprite && !isFire)
        {
            isFire = true;
            Attack();
        }
        else if (spriteRenderer.sprite != shootSprite)
        {
            isFire = false;
        }
        if (!enemyFOV.IsDistancePossible(enemyFOV.ViewRange))
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
        }

    }
    private void CheckBungOff()
    {
        if (enemyFOV.IsDistancePossible(enemyFOV.BungOffRange))
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.BungOff);
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


    protected virtual void AttackEnter()
    {

    }
    protected virtual void Attack()
    {
        StartCoroutine(ReturnToChase());
    }


    private IEnumerator ReturnToChase()
    {
        yield return Yields.WaitUntil(() => spriteRenderer.sprite == endAttackSprite);
        enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
    }

}
