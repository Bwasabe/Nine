using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    protected EnemyAI enemyAI;
    protected EnemyFOV enemyFOV;
    protected EnemyMove enemyMove;


    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        enemyFOV = GetComponentInParent<EnemyFOV>();
        enemyMove = GetComponentInParent<EnemyMove>();
        AddFSM();
    }
    protected virtual void AddFSM()
    {
        //enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, ReturnToChaseAttack);
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Attack, AttackEnter);
        enemyAI.AddFSMAction(FSMStates.FixedUpdate, EnemyAI.States.Attack, ChangeFacing);
        //enemyAI.AddFSMAction(FSMStates.Exit, EnemyAI.States.Attack, Attacking);
        //enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, CheckBungOff);
    }
    protected void Attacking()
    {

        if (!enemyFOV.IsDistancePossible(enemyFOV.ViewRange))
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
        }

    }
    // private void CheckBungOff()
    // {
    //     if (enemyFOV.IsDistancePossible(enemyFOV.BungOffRange))
    //     {
    //         enemyAI.FSM.ChangeState(EnemyAI.States.BungOff);
    //     }
    // }
    protected void ChangeFacing()
    {
        if (GameManager.Instance.Player.transform.position.x > transform.position.x)
        {
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
        }
        enemyMove.SetIsFacingToLocalScale();
    }


    protected virtual void AttackEnter()
    {
        
    }
    protected virtual void Attack()
    {
        Attacking();
    }


    private void ReturnToChase()
    {
        enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
    }

}
