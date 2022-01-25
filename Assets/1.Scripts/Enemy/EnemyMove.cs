using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyMove : CharacterMove
{
    private EnemyAI enemyAI;
    private EnemyFOV enemyFOV;

    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float patrolTime = 2f;

    private float timer;
    protected override void Start()
    {
        base.Start();
        Initialize();
    }
    private void Initialize()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyFOV = GetComponent<EnemyFOV>();
        AddFSM();
    }
    private void AddFSM()
    {
        enemyAI.FSM.ChangeState(EnemyAI.States.Patrol);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, CheckPlatform);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, Move);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ReturnToPatrol);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, ReturnToPatrolAttack);
    }


    private void Move()
    {
        rb.velocity = new Vector2(speed * (isFacingRight ? 1f : -1f), rb.velocity.y);
    }
    private void CheckPlatform()
    {
        if (IsPlatformExist() == false)
        {
            isFacingRight = !isFacingRight;
        }
    }
    private void ReturnToPatrol()
    {
        if (!enemyFOV.IsViewPlayer())
        {
            timer += Time.deltaTime;
            if (timer > patrolTime)
            {
                timer = 0f;
                enemyAI.FSM.ChangeState(EnemyAI.States.Patrol);
            }
        }
        else
        {
            timer = 0f;
        }
    }
    private void ReturnToPatrolAttack()
    {
        if (!enemyFOV.IsViewPlayer())
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Patrol);
        }
    }
    public void SetIsFacingToLocalScale()
    {
        isFacingRight = transform.localScale.x > 0 ? true : false;
    }
    public bool IsPlatformExist()
    {
        return Physics2D.OverlapCircle(new Vector2(isFacingRight ? col.bounds.max.x + 1f : col.bounds.min.x - 1f, col.bounds.min.y), 0.1f, layerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector2(isFacingRight ? col.bounds.max.x + 1f : col.bounds.min.x - 1f, col.bounds.min.y), 0.1f);
    }


}
