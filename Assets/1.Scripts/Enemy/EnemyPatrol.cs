using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyPatrol : EnemyMove
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
        //enemyAI.FSM.ChangeState(EnemyAI.States.Patrol);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, CheckPlatform);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, Move);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ReturnToPatrol);
    }


    private void Move()
    {
        rb.velocity = new Vector2(speed * (isFacingRight ? 1f : -1f), rb.velocity.y);
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


}
