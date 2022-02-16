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
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, IsCanChasePlayer);

    }

private void IsCanChasePlayer()
    {
        if (enemyFOV.IsViewPlayer() && enemyFOV.IsTracePlayer() && enemyFOV.IsDistancePossible(enemyFOV.ViewRange))
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(speed * (isFacingRight ? 1f : -1f), rb.velocity.y);
    }


}
