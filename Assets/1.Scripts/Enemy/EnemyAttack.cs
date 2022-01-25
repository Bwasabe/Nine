using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyAttack : MonoBehaviour
{
    protected EnemyAI enemyAI;
    protected EnemyFOV enemyFOV;

    [SerializeField]
    private int atk;


    private void Start() {
        Initialize();
    }
    protected virtual void Initialize(){
        enemyAI = GetComponent<EnemyAI>();
        enemyFOV = GetComponent<EnemyFOV>();
        AddFSM();
    }
    protected virtual void AddFSM(){
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, CheckAttackPossible);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, ReturnToChaseAttack);
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Attack, Attack);
    }
    private void ReturnToChaseAttack()
    {
        if (!enemyFOV.IsViewPlayer())
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
        }
    }
    private void CheckAttackPossible(){
        if(enemyFOV.IsAttackPossible() && enemyFOV.IsTracePlayer() && enemyFOV.IsViewPlayer()){
            enemyAI.FSM.ChangeState(EnemyAI.States.Attack);
        }
    }
    
    protected virtual void Attack(){
        
    }

    


}
